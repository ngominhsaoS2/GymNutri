var role = {
    countfunction: 0,

    initialize: function () {
        role.getAllPaging(true);

        role.registerControls();

        $.when(role.getAllFunction()).then(function () {
            role.registerEvents();
        });
    },

    registerEvents: function () {
        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdName: { required: true },
                txtmdDescription: { required: true }
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            role.getAllPaging(true);
        });

        $('#btnSearch').off('click').on('click', function (e) {
            role.getAllPaging(true);
        });

        $('#btnErase').off('click').on('click', function (e) {
            role.resetFilters();
            role.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                role.getAllPaging(true);
            }
        });

        $('#btnCreate').off('click').on('click', function (e) {
            role.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            role.getById(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            role.deleteRole(id);
        });

        $('body').on('click', '.btn-grant', function () {
            $('#hidRoleId').val($(this).data('id'));
            $.when(role.getAllFunction())
                .done(role.fillPermission($('#hidRoleId').val()));
            $('#assign-permission-modal').modal('show');
        });

        $('#btnSave').on('click', function (e) {
            role.saveEntity(e);
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-role', id);
        });

        // Event hanlders for Assign Permission Modal
        $('#ckCheckAllView').on('click', function () {
            $('.ckView').prop('checked', $(this).prop('checked'));
        });

        $('#ckCheckAllCreate').on('click', function () {
            $('.ckAdd').prop('checked', $(this).prop('checked'));
        });

        $('#ckCheckAllEdit').on('click', function () {
            $('.ckEdit').prop('checked', $(this).prop('checked'));
        });

        $('#ckCheckAllDelete').on('click', function () {
            $('.ckDelete').prop('checked', $(this).prop('checked'));
        });

        $('body').on('click', '.ckView', function () {
            if ($('.ckView:checked').length == role.countfunction) {
                $('#ckCheckAllView').prop('checked', true);
            } else {
                $('#ckCheckAllView').prop('checked', false);
            }
        });

        $('body').on('click', '.ckAdd', function () {
            if ($('.ckAdd:checked').length == role.countfunction) {
                $('#ckCheckAllCreate').prop('checked', true);
            } else {
                $('#ckCheckAllCreate').prop('checked', false);
            }
        });

        $('body').on('click', '.ckEdit', function () {
            if ($('.ckEdit:checked').length == role.countfunction) {
                $('#ckCheckAllEdit').prop('checked', true);
            } else {
                $('#ckCheckAllEdit').prop('checked', false);
            }
        });

        $('body').on('click', '.ckDelete', function () {
            if ($('.ckDelete:checked').length == role.countfunction) {
                $('#ckCheckAllDelete').prop('checked', true);
            } else {
                $('#ckCheckAllDelete').prop('checked', false);
            }
        });

        $("#btnSavePermission").off('click').on('click', function () {
            role.savePermission();
        });

    },

    registerControls: function () {

    },

    getAllPaging: function (isPageChanged) {
        var template = $('#tbody-role-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            url: '/Admin/Role/GetAllPaging',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                Name: item.Name,
                                Description: item.Description
                            });
                        });

                        if (render != '') {
                            $('#tbody-role').html(render);
                        }
                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            role.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=3><b>There is no data</b></td>';
                        $('#tbody-role').html(render);
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

    resetAddEditModal: function () {
        $('#hidId').val('');
        $('#txtmdName').val('');
        $('#txtmdDescription').val('');
        $('#frmAddEdit').validate().resetForm();
    },

    getById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Role/GetById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    $('#frmAddEdit').validate().resetForm();
                    var data = response.Data;
                    $('#hidId').val(data.Id);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdDescription').val(data.Description);
                    $('#spAddEdit').text('Edit');
                    $('#add-edit-modal').modal('show');
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function () {
                common.notify('error', 'Can not load ajax function GetById');
            }
        });
    },

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/Role/SaveEntity",
                data: {
                    Id: id,
                    Name: $('#txtmdName').val(),
                    Description: $('#txtmdDescription').val()
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        role.getAllPaging(false);
                        role.resetAddEditModal();
                        if (id == 0) {
                            common.notify('success', 'Add Role successfully');
                        }
                        else {
                            common.notify('success', 'Update Role successfully');
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

    deleteRole: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Role/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        role.getAllPaging(true);
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

    getAllFunction: function (callback) {
        var strUrl = "/Admin/Function/GetAll";
        return $.ajax({
            type: "GET",
            url: strUrl,
            dataType: "json",
            beforeSend: function () {
                //commonFunctions.startLoading();
            },
            success: function (response) {
                var template = $('#result-data-function').html();
                var render = "";
                role.countfunction = response.length;
                $.each(response, function (i, item) {
                    render += Mustache.render(template, {
                        Name: item.Name,
                        treegridparent: item.ParentId != null ? "treegrid-parent-" + item.ParentId : "",
                        Id: item.Id
                    });
                });

                if (render != undefined) {
                    $('#lst-data-function').html(render);
                }

                $('.tree').treegrid();

                if (callback != undefined) {
                    callback();
                }

                //commonFunctions.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    },

    fillPermission: function (roleId) {
        var strUrl = "/Admin/Role/ListAllFunction";
        return $.ajax({
            type: "POST",
            url: strUrl,
            data: {
                roleId: roleId
            },
            dataType: "json",
            beforeSend: function () {
                //commonFunctions.stopLoading();
            },
            success: function (response) {
                var litsPermission = response;
                $.each($('#tblFunction tbody tr'), function (i, item) {
                    $.each(litsPermission, function (j, jitem) {
                        if (jitem.FunctionId == $(item).data('id')) {
                            $(item).find('.ckView').first().prop('checked', jitem.CanRead);
                            $(item).find('.ckAdd').first().prop('checked', jitem.CanCreate);
                            $(item).find('.ckEdit').first().prop('checked', jitem.CanUpdate);
                            $(item).find('.ckDelete').first().prop('checked', jitem.CanDelete);
                        }
                    });
                });

                if ($('.ckView:checked').length == $('#tblFunction tbody tr .ckView').length) {
                    $('#ckCheckAllView').prop('checked', true);
                } else {
                    $('#ckCheckAllView').prop('checked', false);
                }
                if ($('.ckAdd:checked').length == $('#tblFunction tbody tr .ckAdd').length) {
                    $('#ckCheckAllCreate').prop('checked', true);
                } else {
                    $('#ckCheckAllCreate').prop('checked', false);
                }
                if ($('.ckEdit:checked').length == $('#tblFunction tbody tr .ckEdit').length) {
                    $('#ckCheckAllEdit').prop('checked', true);
                } else {
                    $('#ckCheckAllEdit').prop('checked', false);
                }
                if ($('.ckDelete:checked').length == $('#tblFunction tbody tr .ckDelete').length) {
                    $('#ckCheckAllDelete').prop('checked', true);
                } else {
                    $('#ckCheckAllDelete').prop('checked', false);
                }

                //commonFunctions.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    },

    savePermission: function () {
        var listPermmission = [];
        $.each($('#tblFunction tbody tr'), function (i, item) {
            listPermmission.push({
                RoleId: $('#hidRoleId').val(),
                FunctionId: $(item).data('id'),
                CanRead: $(item).find('.ckView').first().prop('checked'),
                CanCreate: $(item).find('.ckAdd').first().prop('checked'),
                CanUpdate: $(item).find('.ckEdit').first().prop('checked'),
                CanDelete: $(item).find('.ckDelete').first().prop('checked'),
            });
        });

        $.ajax({
            type: "POST",
            url: "/Admin/Role/SavePermission",
            data: {
                listPermmission: listPermmission,
                roleId: $('#hidRoleId').val()
            },
            beforeSend: function () {
                common.buttonStartLoading('#btnSavePermission');
            },
            success: function (response) {
                common.notify('success', 'Save permission successful');
                $('#assign-permission-modal').modal('hide');
                common.buttonStopLoading('#btnSavePermission');
            },
            error: function () {
                common.notify('error', 'Has an error in save permission progress');
                common.buttonStopLoading('#btnSavePermission');
            }
        });
    },

    resetFilters: function () {
        $('#txtKeyword').val('');
        $('#ulSearched').html('');
    },

}