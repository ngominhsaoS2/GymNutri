var templateMenu = {

    initialize: function () {
        templateMenu.getAllPaging(true);
        templateMenu.registerControls();
        templateMenu.registerEvents();
    },

    registerEvents: function () {

        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdCode: { required: true },
                txtmdName: { required: true },
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            templateMenu.getAllPaging(true);
        });

        $('#btnSearch').off('click').on('click', function (e) {
            templateMenu.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                templateMenu.getAllPaging(true);
            }
        });

        $('#btnErase').off('click').on('click', function (e) {
            templateMenu.resetFilters();
            templateMenu.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            templateMenu.getAllPaging(false);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liSortBy').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liSortBy">Sort: <b>' + selected + '</b></li>');
            }
            else {
                $('#liSortBy').remove();
            }
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-template-menu', id);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            templateMenu.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            templateMenu.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            templateMenu.cloneTemplateMenu(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            templateMenu.deleteTemplateMenu(id);
        });

        // Detail
        $('#btnSave').off('click').on('click', function (e) {
            templateMenu.saveEntity(e);
        });

        $('#btnCreateDetail').off('click').on('click', function (e) {
            $(this).prop('hidden', true);
            $('#btnCancelDetail').prop('hidden', false);
            var selectedIds = $("#ddlmdMeals").val();
            var selectedNames = $('#ddlmdMeals option:selected').map(function () {
                return $(this).text();
            }).get();
            templateMenu.appendDetailControls(selectedIds, selectedNames);
        });

        $('#btnCancelDetail').off('click').on('click', function (e) {
            $(this).prop('hidden', true);
            $('#btnCreateDetail').prop('hidden', false);
            templateMenu.removeDetailControls();
        });

        $('#btnSaveDetail').off('click').on('click', function (e) {
            templateMenu.addDetailToTable();
            templateMenu.changeEnableMeals();
        });

        $('body').on('click', '.btn-detail-save', function (e) {
            e.preventDefault();
            var seleted = $(this);
            var rowId = seleted.data('rowid');
            templateMenu.saveEditingDetail(rowId);
            seleted.prop('hidden', true);
            seleted.next().show();
        });

        $('body').on('click', '.btn-detail-edit', function (e) {
            e.preventDefault();
            var seleted = $(this);
            var rowId = seleted.data('rowid');
            templateMenu.editDetailInTable(rowId);
            seleted.hide();
            seleted.prev().prop('hidden', false);
        });

        $('body').on('click', '.btn-detail-delete', function (e) {
            e.preventDefault();
            $(this).parent().parent().parent().remove();
            templateMenu.changeEnableMeals();
        });

        $('body').on('click', '.btn-detail-clone', function (e) {
            e.preventDefault();
            var rowId = $(this).data('rowid');
            templateMenu.cloneDetail(rowId);
        });
        
    },

    registerControls: function () {
        dropdownlist.getAllMealGroupedData2('#ddlmdMeals');
        templateMenu.getAllBodyClassification('#ddlmdBodyClassifications');
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#template-template-menu').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/TemplateMenu/GetAllPaging',
            dataType: 'JSON',
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                Code: item.Code,
                                Name: item.Name,
                                Description: item.Description
                            });
                        });

                        if (render != '') {
                            $('#body-template-menu').html(render);
                        }
                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            templateMenu.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=5><b>There is no data</b></td>';
                        $('#body-template-menu').html(render);
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function () {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function GetAllPaging');
            }
        });
    },

    resetFilters: function () {
        $('#txtKeyword').val('');
        $('#ddlSortBy').val('');
        $('#ulSearched').html('');
    },

    resetAddEditModal: function () {
        // Header
        $('#hidId').val('0');
        $('#txtmdCode').val('');
        $('#txtmdName').val('');
        $('#txtmdDescription').val('');
        $('#ddlmdMeals').selectpicker('val', []);
        $("#ddlmdMeals").prop('disabled', false);
        $('#frmAddEdit').validate().resetForm();

        // Detail
        $(".removable, .tr-inserted").remove();
        $("#tr-add-controls").addClass('d-none');
    },

    getAllBodyClassification: function (targetId) {
        $.ajax({
            type: 'GET',
            url: '/Admin/BodyClassification/GetAllToGroupedData',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    var data = response.Data;
                    var render = '';
                    if (data != null) {
                        $.each(data, function (index, mother) {
                            render += '<optgroup label="' + mother.text + '">';
                            $.each(mother.children, function (index, child) {
                                render += '<option value="' + child.id + '" data-tokens="' + child.text + '">' + child.text + '</option>';
                            });
                        });
                        $(targetId).html(render);
                        $(targetId).selectpicker({
                            liveSearch: true,
                            size: 10,
                            style: 'form-control-sm btn-light',
                            selectedTextFormat: 'count > 6',
                            width: '100%',
                        });
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function (res) {
                common.notify('error', 'Can not load ajax function GetAllToGroupedData');
            }
        });
    },

    getAllSetOfFood: function (targetId) {
        $(targetId).select2({
            ajax: {
                url: "/Admin/SetOfFood/GetAllPaging",
                dataType: 'JSON',
                delay: 750,
                data: function (params) {
                    return {
                        keyword: params.term,
                        page: params.page || 1,
                        pageSize: 10
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;

                    var select2Data = $.map(data.Data.Results, function (obj) {
                        obj.id = obj.Id;
                        obj.text = '(' + obj.Code + ') ' + obj.ListFoodNames;
                        return obj;
                    });

                    return {
                        results: select2Data,
                        pagination: {
                            more: (params.page * 10) < data.Data.RowCount
                        }
                    };
                },
                cache: true
            },
            theme: 'bootstrap4',
            placeholder: 'Search sets',
            containerCssClass: ':all:',
            allowClear: true,
            dropdownAutoWidth: true,
            width: 'auto',
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 1,
            templateResult: templateMenu.formatSetOfFood,
            templateSelection: templateMenu.formatSetOfFoodSelection
        });
    },

    formatSetOfFood: function (setOfFood) {
        if (setOfFood.loading) {
            return setOfFood.text;
        }

        var markup = "<div class='form-inline'>" +
            "<div>" +
            "<h6>" + setOfFood.Name + " (" + setOfFood.Code + ")" + "</h6>" +
            "<p>" + (setOfFood.ListFoodNames || '') + "</p>" +
            "<p>" + (setOfFood.ListMealNames.replace(/;/g, ' | ') || '') + "</p>" +
            "</div></div>";

        return markup;
    },

    formatSetOfFoodSelection: function (setOfFood) {
        return setOfFood.text;
    },

    appendDetailControls: function (listIds, listNames) {
        $(".removable").remove();
        $("#tr-add-controls").removeClass('d-none');

        $.each(listIds, function (index, value) {
            var sortId = index + 1;
            $("#th-action").before('<th class="removable" data-id="' + sortId + '" data-mealid="' + value + '">' + listNames[index] + '</th>');
            $("#td-action").before('<td class="removable"><select class="input-sm set-of-food adding" id="mealId-' + value + '" name="" data-mealid="' + value + '"></select></td>');
            templateMenu.getAllSetOfFood("#mealId-" + value);
        });
    },

    changeEnableMeals: function () {
        if ($(".tr-inserted").length > 0) {
            $("#ddlmdMeals").prop('disabled', true);
        }
        else {
            $("#ddlmdMeals").prop('disabled', false);
        }
    },

    removeDetailControls: function () {
        // Remove Header and Add controls when having no table row
        if ($(".tr-inserted").length == 0) {
            $(".removable").remove();
        }

        $("#tr-add-controls").addClass('d-none');
    },

    addDetailToTable: function () {
        var template = $('#template-detail').html();
        var number = $(".tr-inserted").length + 1;
        var content = '<td id="row-' + number + '">' + number + '</td>';

        $("select.set-of-food.adding").each(function (index) {
            var mealId = $(this).data('mealid');
            var setOfFoodId = $(this).children("option:selected").val();
            var setOfFoodCode = $(this).children("option:selected").text();
            content += '<td class="td-set" data-mealid="' + mealId + '" data-setoffoodid="' + setOfFoodId + '">' + setOfFoodCode + '</td>';
        });

        var render = Mustache.render(template, {
            TemplateMenuSetId: 0,
            RowId: number,
            Content: content
        });

        $("#tr-add-controls").before(render);

        // Clear controls
        $("select.set-of-food.adding").each(function (index) {
            common.setValueSearchableDropdownlist(this, '');
        });
    },

    editDetailInTable: function (rowId) {
        var selectors = "#tr-" + rowId + " > .td-set";
        $(selectors).each(function (index) {
            var mealId = $(this).data('mealid');
            var setOfFoodId = $(this).data('setoffoodid');
            var setOfFoodCode = $(this).text();
            $(this).html('<select class="input-sm set-of-food editing" id="Row-' + rowId + '-MealId-' + mealId + '" name="" data-mealid="' + mealId + '"></select>');
            $('#Row-' + rowId + '-MealId-' + mealId).html('<option value="' + setOfFoodId + '" selected="selected">' + setOfFoodCode + '</option>');
            templateMenu.getAllSetOfFood('#Row-' + rowId + '-MealId-' + mealId);
        });
    },

    cloneDetail: function (rowId) {
        var number = $(".tr-inserted").length + 1;
        var selectedRowContent = $('#tr-' + rowId).html();
        var newContent = selectedRowContent.replace('<td id="row-' + rowId + '">' + rowId + '</td>', '<td id="row-' + number + '">' + number + '</td>');
        newContent = newContent.replace('text-overflow action', 'text-overflow action-remove');

        var template = $('#template-detail').html();
        var render = Mustache.render(template, {
            TemplateMenuSetId: 0,
            RowId: number,
            Content: newContent
        });

        $('#tr-add-controls').before(render);
        var removeTd = "#tr-" + number + " > .action-remove";
        $(removeTd).remove();
    },

    saveEditingDetail: function (rowId) {
        var selectors = "#tr-" + rowId + " > .td-set > select";
        $(selectors).each(function (index) {
            var setOfFoodId = $(this).children("option:selected").val();
            var setOfFoodCode = $(this).children("option:selected").text();

            var td = $(this).parent();
            td.removeAttr("data-setoffoodid");
            td.attr("data-setoffoodid", setOfFoodId);
            td.html(setOfFoodCode);
        });
    },

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var templateMenuId = $('#hidId').val();
            var code = $('#txtmdCode').val();
            var name = $('#txtmdName').val();
            var description = $('#txtmdDescription').val();
            var selectedMeals = $('#ddlmdMeals').val();
            var meals = selectedMeals != null ? selectedMeals.join(';') : '';

            // TemplateSet
            var templateMenuSets = [];
            $(".tr-inserted").each(function (index) {
                var templateMenuSetId = $(this).data('setid');

                // TemplateSetDetail
                var templateMenuSetDetails = [];
                var rowId = index + 1;
                var selectors = '#tr-' + rowId + ' > .td-set';
                $(selectors).each(function (index) {
                    var mealId = $(this).data('mealid');
                    var setOfFoodId = $(this).data('setoffoodid');
                    var templateMenuSetDetail = { "TemplateMenuSetId": templateMenuSetId || 0, "MealId": mealId, "SetOfFoodId": setOfFoodId, "Active": true };
                    templateMenuSetDetails.push(templateMenuSetDetail);
                });
                
                var templateMenuSet = { "Id": templateMenuSetId || 0, "TemplateMenuId": templateMenuId, "Active": true, TemplateMenuSetDetails: templateMenuSetDetails};
                templateMenuSets.push(templateMenuSet);
            });

            // TemplateMenuForBodyClassification
            var templateMenuForBodyClassifications = [];
            var selectedBodies = $('#ddlmdBodyClassifications').val();
            var bodyClassifications = selectedBodies != null ? selectedBodies.join(';') : '';
            $.each(selectedBodies, function (index, body) {
                var templateMenuForBodyClassification = { "TemplateMenuId": templateMenuId || 0, "BodyClassificationId": body || 0, "InsertedSource": constants.insertedresource.TemplateMenu, "Active": true };
                templateMenuForBodyClassifications.push(templateMenuForBodyClassification);
            });
            
            $.ajax({
                type: "POST",
                url: "/Admin/TemplateMenu/SaveEntity",
                data: {
                    Id: templateMenuId,
                    Code: code,
                    Name: name,
                    Description: description,
                    Meals: meals,
                    BodyClassifications: bodyClassifications,
                    TemplateMenuForBodyClassifications: templateMenuForBodyClassifications,
                    Active: true,
                    TemplateMenuSets: templateMenuSets,
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        templateMenu.getAllPaging(false);
                        templateMenu.resetAddEditModal();
                        if (templateMenuId == 0) {
                            common.notify('success', 'Add Template successfully');
                        }
                        else {
                            common.notify('success', 'Update Template successfully');
                        }
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                    common.buttonStopLoading('#btnSave');
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function SaveEntity');
                    common.buttonStopLoading('#btnSave');
                }
            });

        }
    },

    getById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/TemplateMenu/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    templateMenu.resetAddEditModal();
                    var head = response.Data;

                    // Head
                    $('#hidId').val(head.Id);
                    $('#txtmdCode').val(head.Code);
                    $('#txtmdName').val(head.Name);
                    $('#txtmdDescription').val(head.Description);
                    $('#ddlmdMeals').selectpicker('val', head.Meals != null ? head.Meals.split(';') : []);
                    $('#ddlmdBodyClassifications').selectpicker('val', head.BodyClassifications != null ? head.BodyClassifications.split(';') : []);
                    $('#spAddEdit').text('Edit');
                    $('#add-edit-modal').modal('show');

                    // Detail
                    // Tạo header cho table và adding controls
                    var mealIds = $("#ddlmdMeals").val();
                    var mealNames = $('#ddlmdMeals option:selected').map(function () {
                        return $(this).text();
                    }).get();
                    templateMenu.appendDetailControls(mealIds, mealNames);

                    // Fill dữ liệu vào từng dòng
                    var template = $('#template-detail').html();
                    var templateMenuSets = response.Data.TemplateMenuSets;
                    $.each(templateMenuSets, function (index, templateMenuSet) {
                        var number = index + 1;
                        var content = '<td id="row-' + number + '">' + number + '</td>';

                        $.each(templateMenuSet.TemplateMenuSetDetails, function (index, templateMenuSetDetail) {
                            content +=
                                '<td class="td-set" data-mealid="' + templateMenuSetDetail.MealId + '" data-setoffoodid="' + templateMenuSetDetail.SetOfFoodId + '">' +
                                '(' + templateMenuSetDetail.SetOfFood.Code + ') ' + templateMenuSetDetail.SetOfFood.ListFoodNames +
                                '</td>';
                        });

                        var render = Mustache.render(template, {
                            TemplateMenuSetId: templateMenuSet.Id,
                            RowId: number,
                            Content: content
                        });

                        $("#tr-add-controls").before(render);
                    });

                    templateMenu.changeEnableMeals();
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function () {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function GetById');
            }
        });
    },

    cloneTemplateMenu: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/TemplateMenu/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    templateMenu.resetAddEditModal();
                    var head = response.Data;

                    // Head
                    $('#hidId').val('0');
                    $('#txtmdCode').val(head.Code);
                    $('#txtmdName').val(head.Name);
                    $('#txtmdDescription').val(head.Description);
                    var selectedMeals = head.Meals;
                    if (selectedMeals != null) {
                        $("#ddlmdMeals").val(selectedMeals.split(';')).trigger('change');
                    }
                    $('#spAddEdit').text('Edit');
                    $('#add-edit-modal').modal('show');

                    // Detail
                    // Tạo header cho table và adding controls
                    var mealIds = $("#ddlmdMeals").val();
                    var mealNames = $('#ddlmdMeals option:selected').map(function () {
                        return $(this).text();
                    }).get();
                    templateMenu.appendDetailControls(mealIds, mealNames);

                    // Fill dữ liệu vào từng dòng
                    var template = $('#template-detail').html();
                    var templateMenuSets = response.Data.TemplateMenuSets;
                    $.each(templateMenuSets, function (index, templateMenuSet) {
                        var number = index + 1;
                        var content = '<td id="row-' + number + '">' + number + '</td>';

                        $.each(templateMenuSet.TemplateMenuSetDetails, function (index, templateMenuSetDetail) {
                            content +=
                                '<td class="td-set" data-mealid="' + templateMenuSetDetail.MealId + '" data-setoffoodid="' + templateMenuSetDetail.SetOfFoodId + '">' +
                                '(' + templateMenuSetDetail.SetOfFood.Code + ') ' + templateMenuSetDetail.SetOfFood.ListFoodNames +
                                '</td>';
                        });

                        var render = Mustache.render(template, {
                            TemplateMenuSetId: 0,
                            RowId: number,
                            Content: content
                        });

                        $("#tr-add-controls").before(render);
                    });

                    common.popupNotify('success', 'Close', 'Success', 'Clone this record successfully. Please change some information and save it.');
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function () {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function GetById');
            }
        });
    },

    deleteTemplateMenu: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/TemplateMenu/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        templateMenu.getAllPaging(true);
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function Delete');
                }
            });
        });
    },
}