var userLocation = {

    initialize: function () {
        userLocation.registerControls();
        userLocation.registerEvents();
    },

    registerEvents: function () {
        $('#frmLocation').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtAddress: { required: true },
                txtFirstFrom: { required: true },
                txtFirstTo: { required: true }
            }
        });

        $('#ddlShowPageLocation').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            var userId = $('#hidmdLocationUserId').val();
            userLocation.getAllPagingLocation(userId, true);
        });

        $('#btnSearchLocation').off('click').on('click', function (e) {
            var userId = $('#hidmdLocationUserId').val();
            userLocation.getAllPagingLocation(userId, true);
        });

        $('#txtKeywordLocation').on('keypress', function (e) {
            if (e.which === 13) {
                var userId = $('#hidmdLocationUserId').val();
                userLocation.getAllPagingLocation(userId, true);
            }
        });

        $('body').on('click', '.sort-location', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-location', id);
        });

        $('#btnCreateLocation').off('click').on('click', function () {
            userLocation.resetLocationModal();
            $('#frmLocation').toggle();
            $('#btnSaveLocation').prop('hidden', false);
            $('#btnCancelLocation').prop('hidden', false);
            $('#btnCreateLocation').prop('hidden', true);
        });

        $('#btnCancelLocation').off('click').on('click', function () {
            $('#frmLocation').toggle();
            $('#btnSaveLocation').prop('hidden', true);
            $('#btnCancelLocation').prop('hidden', true);
            $('#btnCreateLocation').prop('hidden', false);
        });

        $("#btnSaveLocation").off('click').on('click', function () {
            userLocation.saveLocation();
        });

        $('body').on('click', '.btn-location-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            userLocation.cloneLocation(id);
        });

        $('body').on('click', '.btn-location-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            userLocation.getLocationById(id);
        });

        $('body').on('click', '.btn-location-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            userLocation.deleteLocation(id);
        });

    },

    registerControls: function () {

    },

    getAllPagingLocation: function (userId, isPageChanged) {
        var template = $('#template-location').html();
        var render = "";
        $('#hidmdLocationUserId').val(userId);

        $.ajax({
            type: 'GET',
            data: {
                userId: userId,
                keyword: $('#txtKeywordLocation').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/Customer/GetAllPagingLocationOfUser',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                Address: item.Address,
                                FirstFrom: item.FirstFrom,
                                FirstTo: item.FirstTo,
                                SecondFrom: item.SecondFrom,
                                SecondTo: item.SecondTo,
                                ThirdFrom: item.ThirdFrom,
                                ThirdTo: item.ThirdTo,
                                Description: item.Description
                            });
                        });

                        if (render != '') {
                            $('#tbody-location').html(render);
                        }

                        common.wrapPaging('#paginationULLocation', response.Data.RowCount, function () {
                            userLocation.getAllPagingLocation(userId, false);
                        }, isPageChanged);

                        $('#lblTotalRecordsLocation').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=6><b>There is no data</b></td>';
                        $('#tbody-location').html(render);
                    }

                    $('#location-modal').modal('show');
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function () {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function GetAllPagingLocationOfUser');
            }
        });
    },

    getLocationById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Customer/GetLocationById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    userLocation.resetLocationModal();
                    var data = response.Data;
                    $('#hidmdLocationId').val(data.Id);
                    $('#txtAddress').val(data.Address);
                    $('#txtDescription').val(data.Description);
                    $('#txtFirstFrom').val(data.FirstFrom);
                    $('#txtFirstTo').val(data.FirstTo);
                    $('#txtSecondFrom').val(data.SecondFrom);
                    $('#txtSecondTo').val(data.SecondTo);
                    $('#txtThirdFrom').val(data.ThirdFrom);
                    $('#txtThirdTo').val(data.ThirdTo);

                    $('#frmLocation').show();
                    $('#btnSaveLocation').prop('hidden', false);
                    $('#btnCancelLocation').prop('hidden', false);
                    $('#btnCreateLocation').prop('hidden', true);
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

    cloneLocation: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Customer/GetLocationById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    userLocation.resetLocationModal();
                    var data = response.Data;
                    $('#hidmdLocationId').val('0');
                    $('#txtAddress').val(data.Address);
                    $('#txtDescription').val(data.Description);
                    $('#txtFirstFrom').val(data.FirstFrom);
                    $('#txtFirstTo').val(data.FirstTo);
                    $('#txtSecondFrom').val(data.SecondFrom);
                    $('#txtSecondTo').val(data.SecondTo);
                    $('#txtThirdFrom').val(data.ThirdFrom);
                    $('#txtThirdTo').val(data.ThirdTo);

                    $('#frmLocation').show();
                    $('#btnSaveLocation').prop('hidden', false);
                    $('#btnCancelLocation').prop('hidden', false);
                    $('#btnCreateLocation').prop('hidden', true);

                    common.popupNotify('success', 'Close', 'Success', 'Clone this record successfully. Please change some information and save it.');
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

    resetLocationModal: function () {
        $('#hidmdLocationId').val('0');
        $('#txtAddress').val('');
        $('#txtDescription').val('');
        $('#txtFirstFrom').val('');
        $('#txtFirstTo').val('');
        $('#txtSecondFrom').val('');
        $('#txtSecondTo').val('');
        $('#txtThirdFrom').val('');
        $('#txtThirdTo').val('');
        $('#frmLocation').validate().resetForm();
    },

    saveLocation: function (e) {
        if ($('#frmLocation').valid()) {
            var id = $('#hidmdLocationId').val();
            var userId = $('#hidmdLocationUserId').val();

            $.ajax({
                type: "POST",
                url: "/Admin/Customer/SaveLocation",
                data: {
                    Id: id,
                    Active: true,
                    UserId: userId,
                    Address: $('#txtAddress').val(),
                    FirstFrom: $('#txtFirstFrom').val(),
                    FirstTo: $('#txtFirstTo').val(),
                    SecondFrom: $('#txtSecondFrom').val(),
                    SecondTo: $('#txtSecondTo').val(),
                    ThirdFrom: $('#txtThirdFrom').val(),
                    ThirdTo: $('#txtThirdTo').val(),
                    Description: $('#txtDescription').val()
                },
                dataType: "JSON",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSaveLocation');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        userLocation.getAllPagingLocation(userId, true);
                        $('#frmLocation').toggle();
                        $('#btnSaveLocation').prop('hidden', true);
                        $('#btnCancelLocation').prop('hidden', true);
                        $('#btnCreateLocation').prop('hidden', false);

                        if (id == 0) {
                            common.notify('success', 'Add successfully');
                        }
                        else {
                            common.notify('success', 'Update successfully');
                        }
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                    common.buttonStopLoading('#btnSaveLocation');
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function SaveLocation');
                    common.buttonStopLoading('#btnSaveLocation');
                }
            });

        }
    },

    deleteLocation: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Customer/DeleteLocation",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        var userId = $('#hidmdLocationUserId').val();
                        userLocation.getAllPagingLocation(userId, true);
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function DeleteLocation');
                }
            });
        });
    },

}