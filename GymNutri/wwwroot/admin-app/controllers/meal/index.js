var meal = {

    initialize: function () {
        meal.getAllPaging(true);
        meal.registerControls();
        meal.registerEvents();
    },

    registerEvents: function () {
        
        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdCode: { required: true },
                txtmdName: { required: true },
                txtmdGroupCode: { required: true },
                txtmdOrder: { required: true },
                txtmdMealTime: { required: true }
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            meal.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            meal.getAllPaging(false);
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
            meal.getAllPaging(true);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liGroupCode').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liGroupCode">GroupCode: <b>' + selected + '</b></li>');
            }
            else {
                $('#liGroupCode').remove();
            }
        });

        $('#btnSearch').off('click').on('click', function (e) {
            meal.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                meal.getAllPaging(true);
            }
        });

        $('#btnErase').off('click').on('click', function (e) {
            meal.resetFilters();
            meal.getAllPaging(true);
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-meal', id);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            meal.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
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

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            meal.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            meal.cloneMeal(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            meal.deleteMeal(id);
        });

        $('#btnSave').on('click', function (e) {
            meal.saveEntity(e);
        });

    },

    registerControls: function () {
        meal.getAllGroupCode();
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#template-meal').html();
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
            url: '/Admin/Meal/GetAllPaging',
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
                                OrderNo: item.OrderNo,
                                Code: item.Code,
                                Name: item.Name,
                                Description: item.Description,
                                MealTime: item.MealTime,
                            });
                        });

                        if (render != '') {
                            $('#body-meal').html(render);
                        }
                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            meal.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=8><b>There is no data</b></td>';
                        $('#body-meal').html(render);
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
        $('#ddlGroupCode').val('');
        $('#ulSearched').html('');
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
        $('#txtmdMealTime').val('');
        $('#frmAddEdit').validate().resetForm();
    },

    getAllGroupCode: function () {
        var render = '<option value="">-- Select --</option>';
        $.ajax({
            type: 'GET',
            url: '/Admin/Meal/GetAllGroupCode',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data != null) {
                        $.each(response.Data, function (i, item) {
                            render += '<option value="' + item.GroupCode + '">' + item.GroupCode + '</option>';
                        });
                        $('#ddlGroupCode').html(render);
                        $('#ddlmdGroupCode').html(render);
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function (res) {
                common.notify('error', 'Can not load ajax function GetAll');
            }
        });
    },

    getById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Meal/GetById",
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
                    $('#txtmdMealTime').val(data.MealTime);
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

    cloneMeal: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Meal/GetById",
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
                    $('#txtmdMealTime').val(data.MealTime);
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

            $.ajax({
                type: "POST",
                url: "/Admin/Meal/SaveEntity",
                data: {
                    Id: id,
                    Code: $('#txtmdCode').val(),
                    Name: name = $('#txtmdName').val(),
                    OrderNo: orderNo = $('#txtmdOrder').val(),
                    GroupCode: $('#txtmdGroupCode').val(),
                    Description: $('#txtmdDescription').val(),
                    MealTime: $('#txtmdMealTime').val(),
                    Active: true
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        meal.getAllPaging(false);
                        meal.resetAddEditModal();
                        if (id == 0) {
                            common.notify('success', 'Add Meal successfully');
                        }
                        else {
                            common.notify('success', 'Update Meal successfully');
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

    deleteMeal: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Meal/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        meal.getAllPaging(true);
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