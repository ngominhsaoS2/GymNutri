var bodyIndex = {
    initialize: function () {
        bodyIndex.registerControls();
        bodyIndex.registerEvents();
    },

    registerEvents: function () {

        $('#frmBodyIndex').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtDate: { required: true }
            }
        });

        $('#ddlShowPageBodyIndex').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            var userId = $('#hidmdUserId').val();
            bodyIndex.getAllPagingBodyIndex(userId, true);
        });

        $('#btnSearchBodyIndex').off('click').on('click', function (e) {
            var userId = $('#hidmdUserId').val();
            bodyIndex.getAllPagingBodyIndex(userId, true);
        });

        $('body').on('click', '.sort-body-index', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-body-index', id);
        });

        $('body').on('click', '.btn-bodyindex-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bodyIndex.cloneBodyIndex(id);
        });

        $('body').on('click', '.btn-bodyindex-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bodyIndex.getBodyIndexById(id);
        });

        $('body').on('click', '.btn-bodyindex-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bodyIndex.deleteBodyIndex(id);
        });

        $('#btnCreateBodyIndex').off('click').on('click', function () {
            bodyIndex.resetBodyIndexModal();
            $('#rDetailInfor').show();
            bodyIndex.showRowBodyClassifications();
            $('#ddlmdBodyClassifications').selectpicker('val', []);
            $('#btnSaveBodyIndex').prop('hidden', false);
            $('#btnCancelBodyIndex').prop('hidden', false);
            $('#btnCreateBodyIndex').prop('hidden', true);
        });

        $('#btnCancelBodyIndex').off('click').on('click', function () {
            $('#rDetailInfor').hide();
            $('#btnSaveBodyIndex').prop('hidden', true);
            //bodyIndex.showRowBodyClassifications();
            $('#btnCancelBodyIndex').prop('hidden', true);
            $('#btnCreateBodyIndex').prop('hidden', false);
        });

        $("#btnSaveBodyIndex").off('click').on('click', function () {
            bodyIndex.saveBodyIndex();
        });

        $('#btnCalculate').off('click').on('click', function () {
            var userBodyIndexId = $('#hidmdBodyIndexId').val();
            bodyIndex.calculateBodyClassification(userBodyIndexId);
        });

        // Slider events
        $("#slHeight").off('slide').on('slide', function (slideEvt) {
            $('#txtHeight').val(slideEvt.value);
        });

        $("#slWeight").off('slide').on('slide', function (slideEvt) {
            $('#txtWeight').val(slideEvt.value);
        });

        $("#slChest").off('slide').on('slide', function (slideEvt) {
            $('#txtChest').val(slideEvt.value);
        });

        $("#slWaist").off('slide').on('slide', function (slideEvt) {
            $('#txtWaist').val(slideEvt.value);
        });

        $("#slBelly").off('slide').on('slide', function (slideEvt) {
            $('#txtBelly').val(slideEvt.value);
        });

        $("#slAss").off('slide').on('slide', function (slideEvt) {
            $('#txtAss').val(slideEvt.value);
        });

        $("#slFat").off('slide').on('slide', function (slideEvt) {
            $('#txtFat').val(slideEvt.value);
        });

        $("#slMuscle").off('slide').on('slide', function (slideEvt) {
            $('#txtMuscle').val(slideEvt.value);
        });

        $('#txtHeight').on('change', function (e) {
            var value = $(this).val();
            $("#slHeight").bootstrapSlider('setValue', value, true);
        });

        $('#txtWeight').on('change', function (e) {
            var value = $(this).val();
            $("#slWeight").bootstrapSlider('setValue', value, true);
        });

        $('#txtChest').on('change', function (e) {
            var value = $(this).val();
            $("#slChest").bootstrapSlider('setValue', value, true);
        });

        $('#txtWaist').on('change', function (e) {
            var value = $(this).val();
            $("#slWaist").bootstrapSlider('setValue', value, true);
        });

        $('#txtBelly').on('change', function (e) {
            var value = $(this).val();
            $("#slBelly").bootstrapSlider('setValue', value, true);
        });

        $('#txtAss').on('change', function (e) {
            var value = $(this).val();
            $("#slAss").bootstrapSlider('setValue', value, true);
        });

        $('#txtFat').on('change', function (e) {
            var value = $(this).val();
            $("#slFat").bootstrapSlider('setValue', value, true);
        });

        $('#txtMuscle').on('change', function (e) {
            var value = $(this).val();
            $("#slMuscle").bootstrapSlider('setValue', value, true);
        });
        // End slider events
    },

    registerControls: function () {
        dropdownlist.getAllBodyClassification('#ddlmdBodyClassifications');
    },

    getAllPagingBodyIndex: function (userId, isPageChanged) {
        var template = $('#template-body-index').html();
        var render = "";
        $('#hidmdUserId').val(userId);

        $.ajax({
            type: 'GET',
            data: {
                userId: userId,
                from: $('#txtFromDate').val(),
                to: $('#txtToDate').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: '/Admin/Customer/GetAllPagingBodyIndexOfUser',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                Date: common.dateFormatJson(item.Date),
                                Height: item.HeightCm,
                                Weight: item.WeightKg,
                                Chest: item.ChestCm,
                                Waist: item.WaistCm,
                                Ass: item.AssCm,
                                FatPercent: item.FatPercent,
                                MusclePercent: item.MusclePercent,
                                IdiWproBmi: item.IdiWproBmi,
                                BodyFat: item.BodyFat
                            });
                        });

                        if (render != '') {
                            $('#tbody-body-index').html(render);
                        }

                        common.wrapPaging('#paginationULBodyIndex', response.Data.RowCount, function () {
                            bodyIndex.getAllPagingBodyIndex(userId, false);
                        }, isPageChanged);

                        $('#lblTotalRecordsBodyIndex').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=10><b>There is no data</b></td>';
                        $('#tbody-body-index').html(render);
                    }

                    $('#body-index-modal').modal('show');
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function () {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function GetAllPagingBodyIndex');
            }
        });
    },

    getBodyIndexById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Customer/GetBodyIndexById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    bodyIndex.resetBodyIndexModal();
                    var data = response.Data;
                    // Boxes
                    $('#hidmdBodyIndexId').val(data.Id);
                    $('#txtmdCode').val(data.Code);
                    $('#txtDate').val(common.dateFormatJson(data.Date));
                    $('#txtHeight').val(data.HeightCm);
                    $('#txtWeight').val(data.WeightKg);
                    $('#txtChest').val(data.ChestCm);
                    $('#txtWaist').val(data.WaistCm);
                    $('#txtBelly').val(data.BellyCm);
                    $('#txtAss').val(data.AssCm);
                    $('#txtFat').val(data.FatPercent);
                    $('#txtMuscle').val(data.MusclePercent);
                    // Sliders
                    $("#slHeight").bootstrapSlider('setValue', data.HeightCm, true);
                    $("#slWeight").bootstrapSlider('setValue', data.WeightKg, true);
                    $("#slChest").bootstrapSlider('setValue', data.ChestCm, true);
                    $("#slWaist").bootstrapSlider('setValue', data.WaistCm, true);
                    $("#slBelly").bootstrapSlider('setValue', data.BellyCm, true);
                    $("#slAss").bootstrapSlider('setValue', data.AssCm, true);
                    $("#slFat").bootstrapSlider('setValue', data.FatPercent, true);
                    $("#slMuscle").bootstrapSlider('setValue', data.MusclePercent, true);
                    // BodyClassification
                    bodyIndex.showRowBodyClassifications();
                    $('#ddlmdBodyClassifications').selectpicker('val', data.BodyClassifications != null ? data.BodyClassifications.split(';') : []);

                    $('#rDetailInfor').show();
                    $('#btnSaveBodyIndex').prop('hidden', false);
                    $('#btnCancelBodyIndex').prop('hidden', false);
                    $('#btnCreateBodyIndex').prop('hidden', true);

                    common.popupNotify('success', 'Close', 'Success', 'Change some information and save it.');
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

    cloneBodyIndex: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Customer/GetBodyIndexById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    //$('#frmAddEdit').validate().resetForm();
                    var data = response.Data;

                    // Boxes
                    $('#hidmdBodyIndexId').val('0');
                    $('#txtmdCode').val(data.Code);
                    $('#txtDate').val(common.dateFormatJson(data.Date));
                    $('#txtHeight').val(data.HeightCm);
                    $('#txtWeight').val(data.WeightKg);
                    $('#txtChest').val(data.ChestCm);
                    $('#txtWaist').val(data.WaistCm);
                    $('#txtBelly').val(data.BellyCm);
                    $('#txtAss').val(data.AssCm);
                    $('#txtFat').val(data.FatPercent);
                    $('#txtMuscle').val(data.MusclePercent);

                    // Sliders
                    $("#slHeight").bootstrapSlider('setValue', data.HeightCm, true);
                    $("#slWeight").bootstrapSlider('setValue', data.WeightKg, true);
                    $("#slChest").bootstrapSlider('setValue', data.ChestCm, true);
                    $("#slWaist").bootstrapSlider('setValue', data.WaistCm, true);
                    $("#slBelly").bootstrapSlider('setValue', data.BellyCm, true);
                    $("#slAss").bootstrapSlider('setValue', data.AssCm, true);
                    $("#slFat").bootstrapSlider('setValue', data.FatPercent, true);
                    $("#slMuscle").bootstrapSlider('setValue', data.MusclePercent, true);
                    // BodyClassification
                    bodyIndex.showRowBodyClassifications();
                    $('#ddlmdBodyClassifications').selectpicker('val', data.BodyClassifications != null ? data.BodyClassifications.split(';') : []);

                    $('#rDetailInfor').show();
                    $('#btnSaveBodyIndex').prop('hidden', false);
                    $('#btnCancelBodyIndex').prop('hidden', false);
                    $('#btnCreateBodyIndex').prop('hidden', true);

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

    saveBodyIndex: function (e) {
        if ($('#frmBodyIndex').valid()) {
            var id = $('#hidmdBodyIndexId').val();
            var userId = $('#hidmdUserId').val();
            var dateArray = $("#txtDate").val().split("/");
            var date = new Date(dateArray[2], dateArray[1] - 1, dateArray[0]);

            var selectedBodies = $('#ddlmdBodyClassifications').val();
            var bodyClassifications = selectedBodies != null ? selectedBodies.join(';') : '';

            $.ajax({
                type: "POST",
                url: "/Admin/Customer/SaveBodyIndex",
                data: {
                    Id: id,
                    Active: true,
                    UserId: userId,
                    Date: date.toLocaleString("en-US"),
                    HeightCm: $('#txtHeight').val(),
                    WeightKg: $('#txtWeight').val(),
                    ChestCm: $('#txtChest').val(),
                    WaistCm: $('#txtWaist').val(),
                    BellyCm: $('#txtBelly').val(),
                    AssCm: $('#txtAss').val(),
                    FatPercent: $('#txtFat').val(),
                    MusclePercent: $('#txtMuscle').val(),
                    BodyClassifications: bodyClassifications
                },
                dataType: "JSON",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSaveBodyIndex');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        bodyIndex.getAllPagingBodyIndex(userId, true);
                        $('#rDetailInfor').hide();
                        $('#btnSaveBodyIndex').prop('hidden', true);
                        $('#btnCancelBodyIndex').prop('hidden', true);
                        $('#btnCreateBodyIndex').prop('hidden', false);

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
                    common.buttonStopLoading('#btnSaveBodyIndex');
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function SaveBodyIndex');
                    common.buttonStopLoading('#btnSaveBodyIndex');
                }
            });

        }
    },

    deleteBodyIndex: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Customer/DeleteBodyIndex",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        var userId = $('#hidmdUserId').val();
                        bodyIndex.getAllPagingBodyIndex(userId, true);
                    }
                    else {
                        common.notify('error', response.Message);
                    }
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function DeleteBodyIndex');
                }
            });
        });
    },

    resetBodyIndexModal: function () {
        $('#hidmdBodyIndexId').val('0');
        $('#txtDate').val('');
        $('#frmBodyIndex').validate().resetForm();
    },

    showRowBodyClassifications: function () {
        var id = $('#hidmdBodyIndexId').val();
        if (id != 0)
            $('#rBodyClassifications').prop('hidden', false);
        else
            $('#rBodyClassifications').prop('hidden', true);
    },

    calculateBodyClassification: function (userBodyIndexId) {
        $.ajax({
            type: "GET",
            url: "/Admin/Customer/CalculateBodyClassification",
            data: {
                userBodyIndexId: userBodyIndexId,
                group: 'BMI'
            },
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    var bodyClassifications = response.Data;
                    $('#ddlmdBodyClassifications').selectpicker('val', bodyClassifications != null ? bodyClassifications.split(';') : []);
                    common.popupNotify('success', 'Close', 'Success', 'Identify the body classifications of user successfully.');
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function () {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function CalculateBodyClassification');
            }
        });
    },
    
}