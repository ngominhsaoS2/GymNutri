var commonCategory = {
    cachedObj : {
        listGroupCode: []
    },

    initialize: function () {
        commonCategory.getAllPaging(true);
        $.when(commonCategory.getAllGroupCode()).then(function () {
            commonCategory.registerControls();
        });
        commonCategory.registerEvents();
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
                txtmdOrder: { required: true }
            }
        });
        
        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            commonCategory.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            commonCategory.getAllPaging(false);
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
            commonCategory.getAllPaging(true);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liGroupCode').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liGroupCode">GroupCode: <b>' + selected + '</b></li>');
            }
            else {
                $('#liGroupCode').remove();
            }
        });

        $('#ddlmdGroupCode').on('change', function () {
            var groupCode = $('#ddlmdGroupCode').val();
            if (groupCode.length > 0) {
                $('#txtmdGroupCode').val(groupCode);
                $('#txtmdGroupCode').prop('disabled', true);
            }
            else {
                $('#txtmdGroupCode').val('');
                $('#txtmdGroupCode').prop('disabled', false);
            }
        });

        $('#btnSearch').off('click').on('click', function (e) {
            commonCategory.getAllPaging(true);
        });

        $('#btnErase').off('click').on('click', function (e) {
            commonCategory.resetFilters();
            commonCategory.getAllPaging(true);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            commonCategory.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('#btnImport').off('click').on('click', function (e) {
            $('#import-excel-modal').modal('show');
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                commonCategory.getAllPaging(true);
            }
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-common-category', id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            commonCategory.deleteCommonCategory(id);
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            commonCategory.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            commonCategory.cloneCommonCategory(id);
        });

        $('#btnSave').on('click', function (e) {
            commonCategory.saveEntity(e);
        });

        $('#btnImportExcel').on('click', function () {
            var fileUpload = $("#fileInputExcel").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append("files", files[i]);
            }

            $.ajax({
                url: '/Admin/CommonCategory/ImportExcel',
                type: 'POST',
                data: fileData,
                beforeSend: function () {
                    common.buttonStartLoading('#btnImportExcel');
                },
                processData: false,  // tell jQuery not to process the data
                contentType: false,  // tell jQuery not to set contentType
                success: function (response) {
                    if (response.Success == true) {
                        $('#import-excel-modal').modal('hide');
                        commonCategory.getAllPaging(true);
                        common.notify('success', 'Import ' + response.Data + ' records successfully');
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                    common.buttonStopLoading('#btnImportExcel');
                },
                error: function () {
                    common.buttonStopLoading('#btnImportExcel');
                    common.notify('error', 'Can not load ajax function ImportExcel');
                }
            });
        });

    },

    registerControls: function () {
        $('#ddlGroupCode').html(commonCategory.initOptionGroupCode(0));
        $('#ddlmdGroupCode').html(commonCategory.initOptionGroupCode(0));
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#template-common-category').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                groupCode: $('#ddlGroupCode').val() == '-- Select category --' ? '' : $('#ddlGroupCode').val(),
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/CommonCategory/GetAllPaging',
            dataType: 'JSON',
            beforeSend: function() {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                GroupCode: item.GroupCode,
                                OrderNo: item.OrderNo,
                                Code: item.Code,
                                Name: item.Name,
                                Description: item.Description
                            });
                        });
                        
                        if (render != '') {
                            $('#body-common-category').html(render);
                        }
                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            commonCategory.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=7><b>There is no data</b></td>';
                        $('#body-common-category').html(render);
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

    getAllGroupCode: function () {
        return $.ajax({
            type: "GET",
            url: "/Admin/CommonCategory/GetAllGroupCode",
            dataType: "json",
            success: function (response) {
                if (response.Success == true) {
                    commonCategory.cachedObj.listGroupCode = response.Data;
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

    initOptionGroupCode: function (selectedId) {
        var render = "<option value=''>-- Select Group --</option>";
        $.each(commonCategory.cachedObj.listGroupCode, function (i, item) {
            if (selectedId === item.GroupCode)
                render += '<option value="' + item.GroupCode + '" selected>' + item.GroupCode + '</option>';
            else
                render += '<option value="' + item.GroupCode + '">' + item.GroupCode + '</option>';
        });
        return render;
    },

    deleteCommonCategory: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/CommonCategory/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        commonCategory.getAllPaging(true);
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

    getById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/CommonCategory/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    $('#frmAddEdit').validate().resetForm();
                    var data = response.Data;
                    $('#hidId').val(data.Id);
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdOrder').val(data.OrderNo);
                    $('#txtmdGroupCode').val(data.GroupCode);
                    $('#txtmdGroupCode').prop('disabled', true);
                    $('#ddlmdGroupCode').val(data.GroupCode);
                    $('#txtmdDescription').val(data.Description);
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

    cloneCommonCategory: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/CommonCategory/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    $('#frmAddEdit').validate().resetForm();
                    var data = response.Data;
                    $('#hidId').val('0');
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdOrder').val(data.OrderNo);
                    $('#txtmdGroupCode').val(data.GroupCode);
                    $('#txtmdGroupCode').prop('disabled', true);
                    $('#ddlmdGroupCode').val(data.GroupCode);
                    $('#txtmdDescription').val(data.Description);
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

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();
            var code = $('#txtmdCode').val();
            var name = $('#txtmdName').val();
            var orderNo = $('#txtmdOrder').val();
            var groupCode = $('#txtmdGroupCode').val();
            var description = $('#txtmdDescription').val();

            $.ajax({
                type: "POST",
                url: "/Admin/CommonCategory/SaveEntity",
                data: {
                    Id: id,
                    Code: code,
                    Name: name,
                    OrderNo: orderNo,
                    GroupCode: groupCode,
                    Description: description,
                    Active: true
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        commonCategory.getAllPaging(false);
                        commonCategory.resetAddEditModal();
                        if (id == 0) {
                            common.notify('success', 'Add Common Category successfully');
                        }
                        else {
                            common.notify('success', 'Update Common Category successfully');
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

    resetAddEditModal: function () {
        $('#hidId').val('0');
        $('#txtmdCode').val('');
        $('#txtmdName').val('');
        $('#txtmdOrder').val('');
        $('#txtmdGroupCode').val('');
        $('#txtmdGroupCode').prop('disabled', false);
        $('#ddlmdGroupCode').val('');
        $('#txtmdDescription').val('');
        $('#frmAddEdit').validate().resetForm();
    },

    resetFilters: function () {
        $('#txtKeyword').val('');
        $('#ddlSortBy').val('');
        $('#ddlGroupCode').val('');
        $('#ulSearched').html('');
    },

}