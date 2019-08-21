var setOfFood = {

    initialize: function () {
        setOfFood.getAllPaging(true);
        setOfFood.registerControls();
        setOfFood.registerEvents();
    },

    registerEvents: function () {
        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdCode: { required: true },
                txtmdName: { required: true },
                ddlmdMeals: { required: true },
            }
        });

        $('#frmAddDetail').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                ddlmdFood: { required: true },
                txtmdQuantity: { required: true },
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            setOfFood.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            setOfFood.getAllPaging(false);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liSortBy').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liSortBy">Sort: <b>' + selected + '</b></li>');
            }
            else {
                $('#liSortBy').remove();
            }
        });

        $('#btnSearch').off('click').on('click', function (e) {
            setOfFood.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                setOfFood.getAllPaging(true);
            }
        });

        $('#btnErase').off('click').on('click', function (e) {
            setOfFood.resetFilters();
            setOfFood.getAllPaging(true);
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-set-of-food', id);
        });

        $('body').on('click', '.sortable-detail', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-detail', id);
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            setOfFood.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            setOfFood.cloneSetOfFood(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            setOfFood.deleteSetOfFood(id);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            setOfFood.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('#btnCreateDetail').off('click').on('click', function (e) {
            $('#rDetail').toggle();
            $(this).prop('hidden', true);
            $('#btnSaveDetail').prop('hidden', false);
            $('#btnCancelDetail').prop('hidden', false);
        });

        $('#btnCancelDetail').off('click').on('click', function (e) {
            $('#rDetail').toggle();
            $(this).prop('hidden', true);
            $('#btnSaveDetail').prop('hidden', true);
            $('#btnCreateDetail').prop('hidden', false);
        });

        $('#btnSaveDetail').off('click').on('click', function (e) {
            var foodId = $('#ddlmdFood option:selected').val();
            var quantity = $('#txtmdQuantity').val();
            setOfFood.appendDetail(foodId, quantity);
        });

        $('#txtmdQuantity').on('keypress', function (e) {
            if (e.which === 13) {
                e.preventDefault();
                var foodId = $('#ddlmdFood option:selected').val();
                var quantity = $('#txtmdQuantity').val();
                setOfFood.appendDetail(foodId, quantity);
            }
        });

        $('body').on('click', '.btn-detail-delete', function (e) {
            e.preventDefault();
            $(this).parent().parent().parent().remove();
        });

        $('#btnSave').on('click', function (e) {
            setOfFood.saveEntity(e);
        });

    },

    registerControls: function () {
        setOfFood.getAllFood();
        dropdownlist.getAllMealGroupedData2('#ddlmdMeals');
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#template-set-of-food').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/SetOfFood/GetAllPaging',
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
                                Description: item.Description,
                                ListMealNames: item.ListMealNames.replace(/;/g, ' | '),
                                ListFoodNames: item.ListFoodNames,
                            });
                        });

                        if (render != '') {
                            $('#body-set-of-food').html(render);
                        }
                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            setOfFood.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=5><b>There is no data</b></td>';
                        $('#body-set-of-food').html(render);
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
        $('#hidId').val('0');
        $('#txtmdCode').val('');
        $('#txtmdName').val('');
        $('#txtmdDescription').val('');
        $('#ddlmdMeals').selectpicker('val', []);
        $('#frmAddEdit').validate().resetForm();
        common.setValueSearchableDropdownlist('#ddlmdFood', '');
        $('#frmAddDetail').validate().resetForm();

        $('#rDetail').hide();
        $('#btnCreateDetail').prop('hidden', false);
        $('#btnCancelDetail').prop('hidden', true);
        $('#btnSaveDetail').prop('hidden', true);
        
        $("#body-detail").html('');
    },

    getAllFood: function () {
        $("#ddlmdFood").select2({
            ajax: {
                url: "/Admin/Food/GetAllPaging",
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
                        obj.text = obj.Name;
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
            placeholder: 'Search foods',
            containerCssClass: ':all:',
            allowClear: true,
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 1,
            templateResult: setOfFood.formatFood,
            templateSelection: setOfFood.formatFoodSelection
        });
    },

    formatFood: function (food) {
        if (food.loading) {
            return food.text;
        }

        var markup = "<div class='form-inline'>" +
            "<div><img class='img-responsive maxh-50' src='" + food.Image + "' /></div>" +
            "<div>" +
            "<h6>" + food.Name + " (" + food.Code + ")" + "</h6>" +
            "<div>" + food.Description || '' + "</div>" +
            "</div></div>";

        return markup;
    },

    formatFoodSelection: function (food) {
        return food.text;
    },

    appendDetail: function (foodId, quantity) {
        if ($('#frmAddDetail').valid()) {
            $.ajax({
                type: "GET",
                url: "/Admin/Food/GetById",
                data: { id: foodId },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        var template = $('#template-detail').html();
                        var render = "";
                        var food = response.Data;
                        render += Mustache.render(template, {
                            Id: food.Id,
                            Image: food.Image,
                            Quantity: quantity,
                            Unit: food.Unit,
                            Code: food.Code,
                            Name: food.Name,
                            Description: food.Description
                        });

                        $("#body-detail").append(render);
                        $('#ddlmdFood').focus();
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function GetById');
                }
            });
        }
    },

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();
            var code = $('#txtmdCode').val();
            var name = $('#txtmdName').val();
            var description = $('#txtmdDescription').val();
            var mealIds = $('#ddlmdMeals').val();
            var mealNames = $('#ddlmdMeals option:selected').map(function () {
                return $(this).text();
            }).get();

            var listFoodIds = [];
            var listQuantities = [];
            var listFoodNames = '';

            $(".food-id").each(function (index) {
                listFoodIds.push($(this).text());
            });

            $(".food-quantity").each(function (index) {
                listQuantities.push($(this).text());
            });

            $(".food-name").each(function (index) {
                var name = $(this);
                var unit = name.prev().prev();
                var quantity = name.prev().prev().prev();
                listFoodNames += quantity.text() + ' ' + unit.text() + ' ' + name.text() + '; \n';
            });

            $.ajax({
                type: "POST",
                url: "/Admin/SetOfFood/SaveEntity",
                data: {
                    Id: id,
                    Code: code,
                    Name: name,
                    Description: description,
                    ListFoodNames: listFoodNames,
                    ListMealIds: mealIds.join(';'),
                    ListMealNames: mealNames.join(';'),
                    Active: true,
                    listFoodIds: listFoodIds,
                    listQuantities: listQuantities
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        setOfFood.getAllPaging(false);
                        if (id == 0) {
                            common.notify('success', 'Add Set of Foods successfully');
                        }
                        else {
                            common.notify('success', 'Update Set of Foods successfully');
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
            url: "/Admin/SetOfFood/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    setOfFood.resetAddEditModal();
                    var head = response.Data;

                    // Head
                    $('#hidId').val(head.Id);
                    $('#txtmdCode').val(head.Code);
                    $('#txtmdName').val(head.Name);
                    $('#txtmdDescription').val(head.Description);
                    $('#ddlmdMeals').selectpicker('val', head.ListMealIds != null ? head.ListMealIds.split(';') : []);
                    $('#spAddEdit').text('Edit');
                    $('#add-edit-modal').modal('show');

                    // Detail
                    var template = $('#template-detail').html();
                    var render = "";
                    var detail = response.Data.FoodsInSets;

                    $.each(detail, function (i, item) {
                        render += Mustache.render(template, {
                            Id: item.Food.Id,
                            Image: item.Food.Image,
                            Quantity: item.Quantity,
                            Unit: item.Food.Unit,
                            Code: item.Food.Code,
                            Name: item.Food.Name,
                            Description: item.Food.Description
                        });
                    });
                    
                    $("#body-detail").html(render);
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

    cloneSetOfFood: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/SetOfFood/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    setOfFood.resetAddEditModal();
                    var head = response.Data;

                    // Head
                    $('#hidId').val('0');
                    $('#txtmdCode').val(head.Code);
                    $('#txtmdName').val(head.Name);
                    $('#txtmdDescription').val(head.Description);
                    $('#ddlmdMeals').selectpicker('val', head.ListMealIds != null ? head.ListMealIds.split(';') : []);
                    $('#spAddEdit').text('Edit');
                    $('#add-edit-modal').modal('show');

                    // Detail
                    var template = $('#template-detail').html();
                    var render = "";
                    var detail = response.Data.FoodsInSets;
                    $.each(detail, function (i, item) {
                        render += Mustache.render(template, {
                            Id: item.Food.Id,
                            Image: item.Food.Image,
                            Quantity: item.Quantity,
                            Code: item.Food.Code,
                            Name: item.Food.Name,
                            Description: item.Food.Description
                        });
                    });
                    $("#body-detail").html(render);

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

    deleteSetOfFood: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/SetOfFood/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        setOfFood.getAllPaging(true);
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