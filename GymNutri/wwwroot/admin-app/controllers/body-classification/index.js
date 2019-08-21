var bodyClassification = {

    initialize: function () {
        bodyClassification.getAllPaging(true);
        bodyClassification.registerControls();
        bodyClassification.registerEvents();
    },

    registerEvents: function () {
        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdGroupCode: { required: true },
                txtmdCode: { required: true },
                txtmdName: { required: true },
            }
        });

        $('#btnSearch').off('click').on('click', function (e) {
            bodyClassification.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                bodyClassification.getAllPaging(true);
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            bodyClassification.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            bodyClassification.getAllPaging(false);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liSortBy').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liSortBy">Sort: <b>' + selected + '</b></li>');
            }
            else {
                $('#liSortBy').remove();
            }
        });

        $('#ddlGroupCode').on('change', function () {
            bodyClassification.getAllPaging(false);
            var selected = $("#ddlGroupCode option:selected");
            if (selected.val() != ' ') {
                $('#liGroupCode').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liGroupCode">Group: <b>' + selected.text() + '</b></li>');
            }
            else {
                $('#liGroupCode').remove();
            }
        });

        $('#btnErase').off('click').on('click', function (e) {
            bodyClassification.resetFilters();
            bodyClassification.getAllPaging(true);
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-body-classification', id);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            bodyClassification.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('#ddlmdGroupCode').on('change', function () {
            var groupCode = $('#ddlmdGroupCode').val();
            if (groupCode == ' ') {
                $('#txtmdGroupCode').val('');
                $('#txtmdGroupCode').prop('disabled', false);
            }
            else {
                $('#txtmdGroupCode').val(groupCode);
                $('#txtmdGroupCode').prop('disabled', true);
            }
        });

        $('#btnSave').on('click', function (e) {
            bodyClassification.saveEntity(e);
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bodyClassification.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bodyClassification.cloneBodyClassification(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bodyClassification.deleteBodyClassification(id);
        });
        
    },

    registerControls: function () {
        bodyClassification.getAllGroupCode('.ddl-group-code', '', true);
        dropdownlist.getAllTemplateMenu('#ddlmdTemplateMenus');
    },
    
    getAllGroupCode: function (targetId, selectedValue, isSearchable) {
        return $.ajax({
            type: "GET",
            url: "/Admin/BodyClassification/GetAllGroupCode",
            dataType: "json",
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data != null) {
                        var data = response.Data;
                        var render = '<option value=" ">-- Select Group --</option>';
                        $.each(data, function (i, item) {
                            if (selectedValue == item.GroupCode)
                                render += '<option value="' + item.GroupCode + '" selected>' + item.GroupCode + '</option>';
                            else
                                render += '<option value="' + item.GroupCode + '">' + item.GroupCode + '</option>';
                        });
                        $(targetId).html(render);

                        if (isSearchable) {
                            common.selectSearch(targetId);
                        }
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function () {
                common.notify('error', 'Can not load ajax function GetAllGroupCode');
            }
        });
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#template-body-classification').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                groupCode: $('#ddlGroupCode').val(),
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/BodyClassification/GetAllPaging',
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
                                GroupCode: item.GroupCode,
                                Code: item.Code,
                                Name: item.Name,
                                Criterion: item.Criterion,
                                Description: item.Description
                            });
                        });

                        if (render != '') {
                            $('#body-body-classification').html(render);
                        }
                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            bodyClassification.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=8><b>There is no data</b></td>';
                        $('#body-body-classification').html(render);
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
        common.setValueSearchableDropdownlist('#ddlGroupCode', '');
        $('#ulSearched').html('');
    },

    resetAddEditModal: function () {
        $('#hidId').val('0');
        $('#txtmdCode').val('');
        $('#txtmdName').val('');
        $('#txtmdGroupCode').val('');
        $('#txtmdGroupCode').prop('disabled', false);
        common.setValueSearchableDropdownlist('#ddlmdGroupCode', '');
        $('#ddlmdTemplateMenus').selectpicker('val', []);
        $('#txtmdDescription').val('');
        $('#txtmdDetail').val('');
        $('#txtmdCriterion').val('');
        $('#frmAddEdit').validate().resetForm();
    },

    getById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/BodyClassification/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    bodyClassification.resetAddEditModal();
                    var data = response.Data;
                    $('#hidId').val(data.Id);
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdGroupCode').val(data.GroupCode);
                    $('#txtmdGroupCode').prop('disabled', true);
                    $('#ddlmdGroupCode').val(data.GroupCode);
                    common.setValueSearchableDropdownlist('#ddlmdGroupCode', data.GroupCode);
                    $('#ddlmdTemplateMenus').selectpicker('val', data.ListTemplateMenuIds != null ? data.ListTemplateMenuIds.split(';') : []);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdDetail').val(data.Detail);
                    $('#txtmdCriterion').val(data.Criterion);
                    $('#spAddEdit').text('Edit');
                    $('#add-edit-modal').modal('show');
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

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var bodyId = $('#hidId').val();
            var selectedTemplates = $('#ddlmdTemplateMenus').val();
            var listTemplateMenuIds = selectedTemplates != null ? selectedTemplates.join(';') : '';
            
            // TemplateMenuForBodyClassifications
            var templateMenuForBodyClassifications = [];
            $.each(selectedTemplates, function (index, template) {
                var templateMenuForBodyClassification = { "TemplateMenuId": template || 0, "BodyClassificationId": bodyId || '', "InsertedSource": constants.insertedresource.BodyClassification, "Active": true };
                templateMenuForBodyClassifications.push(templateMenuForBodyClassification);
            });
            
            $.ajax({
                type: "POST",
                url: "/Admin/BodyClassification/SaveEntity",
                data: {
                    Id: bodyId,
                    Code: $('#txtmdCode').val(),
                    Name: $('#txtmdName').val(),
                    GroupCode: $('#txtmdGroupCode').val(),
                    Description: $('#txtmdDescription').val(),
                    ListTemplateMenuIds: listTemplateMenuIds,
                    TemplateMenuForBodyClassifications: templateMenuForBodyClassifications,
                    Detail: $('#txtmdDetail').val(),
                    Criterion: $('#txtmdCriterion').val(),
                    Active: true
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        bodyClassification.getAllPaging(false);
                        bodyClassification.resetAddEditModal();
                        if (bodyId == 0) {
                            common.notify('success', 'Add Body Classification successfully');
                        }
                        else {
                            common.notify('success', 'Update Body Classification successfully');
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

    deleteBodyClassification: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/BodyClassification/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        bodyClassification.getAllPaging(true);
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

    cloneBodyClassification: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/BodyClassification/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    bodyClassification.resetAddEditModal();
                    var data = response.Data;
                    $('#hidId').val('0');
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdGroupCode').val(data.GroupCode);
                    $('#txtmdGroupCode').prop('disabled', true);
                    $('#ddlmdGroupCode').val(data.GroupCode);
                    common.setValueSearchableDropdownlist('#ddlmdGroupCode', data.GroupCode);
                    $('#ddlmdTemplateMenus').selectpicker('val', data.ListTemplateMenuIds != null ? data.ListTemplateMenuIds.split(';') : []);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdDetail').val(data.Detail);
                    $('#txtmdCriterion').val(data.Criterion);
                    $('#spAddEdit').text('Edit');
                    $('#add-edit-modal').modal('show');

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

}