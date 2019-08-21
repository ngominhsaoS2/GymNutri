var memberCard = {

    initialize: function () {
        memberCard.getAllPaging(true);
        memberCard.registerControls();
        memberCard.registerEvents();
    },

    registerEvents: function () {

        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                ddlmdUser: { required: true },
                txtmdStartDate: {
                    required: true,
                    lessDate: ['#txtmdEndDate']
                },
                txtmdEndDate: {
                    required: true,
                    greateDate: ['#txtmdStartDate']
                },
                ddlmdMemberType: { required: true },
                ddlmdTemplateMenu: { required: true },
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            memberCard.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            memberCard.getAllPaging(false);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liSortBy').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liSortBy">Sort: <b>' + selected + '</b></li>');
            }
            else {
                $('#liSortBy').remove();
            }
        });

        $('#ddlMemberTypeCode').on('change', function () {
            memberCard.getAllPaging(false);
            var selected = $('#ddlMemberTypeCode option:selected');
            if (selected.val().length > 0) {
                $('#liMemberType').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liMemberType">MemberType: <b>' + selected.text() + '</b></li>');
            }
            else {
                $('#liMemberType').remove();
            }
        });

        $('#txtStartDate').on('change', function () {
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liStartDate').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liStartDate">StartDate: <b>' + selected + '</b></li>');
            }
            else {
                $('#liStartDate').remove();
            }
        });

        $('#txtEndDate').on('change', function () {
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liEndDate').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liStartDate">EndDate: <b>' + selected + '</b></li>');
            }
            else {
                $('#liEndDate').remove();
            }
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-member-card', id);
        });

        $('#btnSearch').off('click').on('click', function (e) {
            memberCard.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                memberCard.getAllPaging(true);
            }
        });

        $('#btnErase').off('click').on('click', function (e) {
            memberCard.resetFilters();
            memberCard.getAllPaging(true);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            memberCard.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('#txtmdStartDate').on('change', function () {
            // StartDate
            var startArray = $('#txtmdStartDate').val().split("/");
            var startDate = new Date(startArray[2], startArray[1] - 1, startArray[0]);

            // EndDate
            var endArray = $('#txtmdEndDate').val().split("/");
            var endDate = new Date(endArray[2], endArray[1] - 1, endArray[0]);
            
            var months = common.diffMonths(startDate, endDate);
            $('#txtmdMonthDuration').val(months);
        });

        $('#txtmdEndDate').on('change', function () {
            // StartDate
            var startArray = $('#txtmdStartDate').val().split("/");
            var startDate = new Date(startArray[2], startArray[1] - 1, startArray[0]);

            // EndDate
            var endArray = $('#txtmdEndDate').val().split("/");
            var endDate = new Date(endArray[2], endArray[1] - 1, endArray[0]);

            var months = common.diffMonths(startDate, endDate);
            $('#txtmdMonthDuration').val(months);
        });

        $('#btnSave').on('click', function (e) {
            memberCard.saveEntity(e);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            memberCard.deleteMemberCard(id);
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            memberCard.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            memberCard.cloneMemberCard(id);
        });

    },

    registerControls: function () {
        common.setupDatePicker('.datepicker', 'dd/mm/yyyy');
        dropdownlist.getCategoriesByGroupCode(constants.groupcode.MemberType, '.member-type', '');
        dropdownlist.getAllUser('#ddlmdUser');
        dropdownlist.getAllTemplateMenu('#ddlmdTemplateMenu');
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#template-member-card').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                userId: '',
                startDate: $('#txtStartDate').val(),
                endDate: $('#txtEndDate').val(),
                memberTypeCode: $('#ddlMemberTypeCode').val(),
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/MemberCard/GetAllPaging',
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
                                StartDate: common.dateFormatJson(item.StartDate),
                                EndDate: common.dateFormatJson(item.EndDate),
                                MemberTypeCode: item.MemberTypeCode,
                                User: item.AppUser.Email,
                                Template: item.TemplateMenu.Code,
                                MonthDuration: item.MonthDuration
                            });
                        });

                        if (render != '') {
                            $('#body-member-card').html(render);
                        }
                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            memberCard.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=8><b>There is no data</b></td>';
                        $('#body-member-card').html(render);
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
        $('#ddlMemberTypeCode').val('');
        $('#txtStartDate').val('');
        $('#txtEndDate').val('');
        $('#ulSearched').html('');
    },

    resetAddEditModal: function () {
        $('#hidId').val('0');
        $('#txtmdStartDate').val('');
        $('#txtmdEndDate').val('');
        $('#txtmdMonthDuration').val('0');
        $('#ddlmdUser').html('');
        $('#ddlmdMemberType').val('');
        $('#txtmdDescription').val('');
        $('#frmAddEdit').validate().resetForm();
    },

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();

            var startArray = $("#txtmdStartDate").val().split("/");
            var startDate = new Date(startArray[2], startArray[1] - 1, startArray[0]);
            var endArray = $("#txtmdEndDate").val().split("/");
            var endDate = new Date(endArray[2], endArray[1] - 1, endArray[0]);

            $.ajax({
                type: "POST",
                url: "/Admin/MemberCard/SaveEntity",
                data: {
                    Id: id,
                    UserId: $('#ddlmdUser option:selected').val(),
                    MemberTypeCode: $('#ddlmdMemberType').val(),
                    StartDate: startDate.toLocaleString("en-US"),
                    EndDate: endDate.toLocaleString("en-US"),
                    MonthDuration: $('#txtmdMonthDuration').val(),
                    TemplateMenuId: $('#ddlmdTemplateMenu option:selected').val(),
                    Cost: 0,
                    Price: 0,
                    PromotionPrice: 0,
                    Description: $('#txtmdDescription').val(),
                    Active: true
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        memberCard.getAllPaging(true);
                        memberCard.resetAddEditModal();
                        if (id == 0) {
                            common.notify('success', 'Add Member Card successfully');
                        }
                        else {
                            common.notify('success', 'Update Member Card successfully');
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

    deleteMemberCard: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/MemberCard/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        memberCard.getAllPaging(true);
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
            url: "/Admin/MemberCard/GetById",
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
                    $('#txtmdStartDate').val(common.dateFormatJson(data.StartDate));
                    $('#txtmdEndDate').val(common.dateFormatJson(data.EndDate));
                    $('#txtmdMonthDuration').val(data.MonthDuration);

                    // User
                    $('#ddlmdUser').html('<option value="' + data.UserId + '" selected="selected">' + data.AppUser.Email + '</option>');
                    dropdownlist.getAllUser('#ddlmdUser');

                    $('#ddlmdMemberType').val(data.MemberTypeCode);
                    $('#ddlmdTemplateMenu').selectpicker('val', data.TemplateMenuId > 0 ? [data.TemplateMenuId] : []);
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

    cloneMemberCard: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/MemberCard/GetById",
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
                    $('#txtmdStartDate').val(common.dateFormatJson(data.StartDate));
                    $('#txtmdEndDate').val(common.dateFormatJson(data.EndDate));
                    $('#txtmdMonthDuration').val(data.MonthDuration);

                    // User
                    $('#ddlmdUser').html('<option value="' + data.UserId + '" selected="selected">' + data.AppUser.Email + '</option>');
                    dropdownlist.getAllUser('#ddlmdUser');

                    $('#ddlmdMemberType').val(data.MemberTypeCode);
                    $('#ddlmdTemplateMenu').selectpicker('val', data.TemplateMenuId > 0 ? [data.TemplateMenuId] : []);
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
    }

}