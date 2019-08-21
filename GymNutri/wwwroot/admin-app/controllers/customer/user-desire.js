var userDesire = {
    initialize: function () {
        userDesire.registerControls();
        userDesire.registerEvents();
    },

    registerEvents: function () {
        $('#frmDesire').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                ddlmdDesireTypeCode: { required: true },
                ddlmdChangingSpeed: { required: true },
                ddlmdPracticeIntensive: { required: true },
                txtmdStartDate: { required: true },
                txtmdEndDate: { required: true },
                txtmdDescription: { required: true },
                txtmdDetail: { required: true }
            }
        });

        $('#ddlShowPageDesire').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            var userId = $('#hidmdDesireUserId').val();
            userDesire.getAllPagingDesire(userId, true);
        });

        $('#btnSearchDesire').off('click').on('click', function (e) {
            var userId = $('#hidmdDesireUserId').val();
            userDesire.getAllPagingDesire(userId, true);
        });

        $('#txtKeywordDesire').on('keypress', function (e) {
            if (e.which === 13) {
                var userId = $('#hidmdDesireUserId').val();
                userDesire.getAllPagingDesire(userId, true);
            }
        });

        $('body').on('click', '.sort-desire', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-desire', id);
        });

        $('#btnCreateDesire').off('click').on('click', function () {
            userDesire.resetDesireModal();
            $('#frmDesire').toggle();
            $('#btnSaveDesire').prop('hidden', false);
            $('#btnCancelDesire').prop('hidden', false);
            $('#btnCreateDesire').prop('hidden', true);
        });

        $('#btnCancelDesire').off('click').on('click', function () {
            $('#frmDesire').toggle();
            $('#btnSaveDesire').prop('hidden', true);
            $('#btnCancelDesire').prop('hidden', true);
            $('#btnCreateDesire').prop('hidden', false);
        });

        $('body').on('click', '.btn-desire-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            userDesire.getDesireById(id);
        });

        $('body').on('click', '.btn-desire-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            userDesire.cloneDesire(id);
        });

        $('body').on('click', '.btn-desire-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            userDesire.deleteDesire(id);
        });

        $("#btnSaveDesire").off('click').on('click', function () {
            userDesire.saveDesire();
        });

    },
    
    registerControls: function () {
        dropdownlist.getCategoriesByGroupCode(constants.groupcode.DesireType, '#ddlmdDesireTypeCode', '');
        dropdownlist.getCategoriesByGroupCode(constants.groupcode.Speed, '#ddlmdChangingSpeed', '');
        dropdownlist.getCategoriesByGroupCode(constants.groupcode.PracticeIntensive, '#ddlmdPracticeIntensive', '');
    },
    
    getAllPagingDesire: function (userId, isPageChanged) {
        var template = $('#template-desire').html();
        var render = "";
        $('#hidmdDesireUserId').val(userId);

        $.ajax({
            type: 'GET',
            data: {
                userId: userId,
                keyword: $('#txtKeywordDesire').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/Customer/GetAllPagingDesireOfUser',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                StartDate: common.dateFormatJson(item.StartDate),
                                EndDate: common.dateFormatJson(item.EndDate),
                                DesireTypeCode: item.DesireTypeCode,
                                ChangingSpeed: item.ChangingSpeed,
                                PracticeIntensive: item.PracticeIntensive,
                                Description: item.Description,
                                Detail: item.Detail
                            });
                        });

                        if (render != '') {
                            $('#tbody-desire').html(render);
                        }

                        common.wrapPaging('#paginationULDesire', response.Data.RowCount, function () {
                            userDesire.getAllPagingDesire(userId, false);
                        }, isPageChanged);

                        $('#lblTotalRecordsDesire').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=6><b>There is no data</b></td>';
                        $('#tbody-desire').html(render);
                    }

                    $('#desire-modal').modal('show');
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function () {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function GetAllPagingDesireOfUser');
            }
        });
    },

    getDesireById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Customer/GetDesireById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    userDesire.resetDesireModal();
                    var data = response.Data;
                    $('#hidmdDesireId').val(data.Id);
                    $('#ddlmdDesireTypeCode').val(data.DesireTypeCode);
                    $('#ddlmdChangingSpeed').val(data.ChangingSpeed);
                    $('#ddlmdPracticeIntensive').val(data.PracticeIntensive);
                    $('#txtmdStartDate').val(common.dateFormatJson(data.StartDate));
                    $('#txtmdEndDate').val(common.dateFormatJson(data.EndDate));
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdDetail').val(data.Detail);

                    $('#frmDesire').show();
                    $('#btnSaveDesire').prop('hidden', false);
                    $('#btnCancelDesire').prop('hidden', false);
                    $('#btnCreateDesire').prop('hidden', true);
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

    cloneDesire: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Customer/GetDesireById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    userDesire.resetDesireModal();
                    var data = response.Data;
                    $('#hidmdDesireId').val('0');
                    $('#ddlmdDesireTypeCode').val(data.DesireTypeCode);
                    $('#ddlmdChangingSpeed').val(data.ChangingSpeed);
                    $('#ddlmdPracticeIntensive').val(data.PracticeIntensive);
                    $('#txtmdStartDate').val(common.dateFormatJson(data.StartDate));
                    $('#txtmdEndDate').val(common.dateFormatJson(data.EndDate));
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdDetail').val(data.Detail);

                    $('#frmDesire').show();
                    $('#btnSaveDesire').prop('hidden', false);
                    $('#btnCancelDesire').prop('hidden', false);
                    $('#btnCreateDesire').prop('hidden', true);

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

    resetDesireModal: function () {
        $('#hidmdDesireId').val('0');
        $('#ddlmdDesireTypeCode').val('');
        $('#ddlmdChangingSpeed').val('');
        $('#ddlmdPracticeIntensive').val('');
        $('#txtmdStartDate').val('');
        $('#txtmdEndDate').val('');
        $('#txtmdDescription').val('');
        $('#txtmdDetail').val('');
        $('#frmDesire').validate().resetForm();
    },

    saveDesire: function (e) {
        if ($('#frmDesire').valid()) {
            var id = $('#hidmdDesireId').val();
            var userId = $('#hidmdDesireUserId').val();
            var startDateArray = $("#txtmdStartDate").val().split("/");
            var endDateArray = $("#txtmdEndDate").val().split("/");
            var startDate = new Date(startDateArray[2], startDateArray[1] - 1, startDateArray[0]);
            var endDate = new Date(endDateArray[2], endDateArray[1] - 1, endDateArray[0]);

            $.ajax({
                type: "POST",
                url: "/Admin/Customer/SaveDesire",
                data: {
                    Id: id,
                    Active: true,
                    UserId: userId,
                    StartDate: startDate.toLocaleString("en-US"),
                    EndDate: endDate.toLocaleString("en-US"),
                    DesireTypeCode: $('#ddlmdDesireTypeCode').val(),
                    ChangingSpeed: $('#ddlmdChangingSpeed').val(),
                    PracticeIntensive: $('#ddlmdPracticeIntensive').val(),
                    Description: $('#txtmdDescription').val(),
                    Detail: $('#txtmdDetail').val()
                },
                dataType: "JSON",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSaveDesire');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        userDesire.getAllPagingDesire(userId, true);
                        $('#frmDesire').toggle();
                        $('#btnSaveDesire').prop('hidden', true);
                        $('#btnCancelDesire').prop('hidden', true);
                        $('#btnCreateDesire').prop('hidden', false);

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
                    common.buttonStopLoading('#btnSaveDesire');
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function SaveDesire');
                    common.buttonStopLoading('#btnSaveDesire');
                }
            });

        }
    },

    deleteDesire: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Customer/DeleteDesire",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        var userId = $('#hidmdDesireUserId').val();
                        userDesire.getAllPagingDesire(userId, true);
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function DeleteDesire');
                }
            });
        });
    },

}