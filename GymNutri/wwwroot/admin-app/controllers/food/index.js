var food = {
    initialize: function () {
        food.getAllPaging(true);
        food.registerControls();
        food.registerEvents();
    },

    registerEvents: function () {
        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdCode: { required: true },
                txtmdName: { required: true },
                ddlmdFoodCategory: { required: true },
                ddlmdUnit: { required: true },
                txtmdDescription: { required: true },
                txtmdCookingDuration: { required: true }
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            food.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            food.getAllPaging(false);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liSortBy').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liSortBy">Sort: <b>' + selected + '</b></li>');
            }
            else {
                $('#liSortBy').remove();
            }
        });

        $('#ddlCategory').on('change', function () {
            food.getAllPaging(true);
            var selected = $("#ddlCategory option:selected").text();
            if (selected.length > 0) {
                $('#liCategory').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liCategory">Category: <b>' + selected + '</b></li>');
            }
            else {
                $('#liCategory').remove();
            }
        });

        $('#btnSearch').off('click').on('click', function (e) {
            food.getAllPaging(true);
        });

        $('#btnErase').off('click').on('click', function (e) {
            food.resetFilters();
            food.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                food.getAllPaging(true);
            }
        });

        $('#btnCreate').off('click').on('click', function (e) {
            food.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });

        $("#fileInputImage").on('change', function () {
            food.uploadImage(this);
        });

        $('#btnSave').on('click', function (e) {
            food.saveEntity(e);
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-food', id);
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            food.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            food.cloneFood(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            food.deleteFood(id);
        });

        $('body').on('click', '.btn-images', function (e) {
            $('#images-modal').modal('show');
        });

    },

    registerControls: function () {
        food.getAllFoodCategory();
        food.getAllUnits('#ddlmdUnit', '');
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#tbody-food-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                categoryId: $('#ddlCategory').val(),
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            url: '/Admin/Food/GetAllPaging',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                Code: item.Code,
                                Name: item.Name,
                                Description: item.Description,
                                CookingDuration: item.CookingDuration,
                                Image: item.Image
                            });
                        });

                        if (render != '') {
                            $('#tbody-food').html(render);
                        }

                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            food.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=8><b>There is no data</b></td>';
                        $('#tbody-food').html(render);
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
                common.stopLoading('#preloader');
            },
            error: function (res) {
                common.stopLoading('#preloader');
                common.notify('error', 'Can not load ajax function GetAllPaging');
            }
        });
    },

    getAllFoodCategory: function () {
        var render = '<option value=" ">-- Select --</option>';
        $.ajax({
            type: 'GET',
            url: '/Admin/FoodCategory/GetAll',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data != null) {
                        $.each(response.Data, function (i, item) {
                            render += '<option data-code="' + item.Code + '" value="' + item.Id + '">' + item.Code + ' - ' + item.Name + '</option>';
                        });
                        $('#ddlCategory').html(render);
                        common.selectSearch('#ddlCategory');
                        $('#ddlmdFoodCategory').html(render);
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

    getAllUnits: function (targetId, selectedId) {
        return $.ajax({
            type: "GET",
            url: "/Admin/CommonCategory/GetByGroupCode",
            dataType: "json",
            data: {
                groupCode: constants.groupcode.Unit
            },
            success: function (response) {
                if (response.Success == true) {
                    var render = "<option value=''>-- Select --</option>";
                    $.each(response.Data, function (i, item) {
                        if (selectedId === item.Code)
                            render += '<option value="' + item.Name + '" selected>' + item.Name + '</option>';
                        else
                            render += '<option value="' + item.Name + '">' + item.Name + '</option>';
                    });

                    $(targetId).html(render);
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function () {
                common.notify('error', 'Can not load ajax function GetByGroupCode');
            }
        });
    },

    resetFilters: function () {
        $('#txtKeyword').val('');
        $('#ddlSortBy').val('');
        common.setValueSearchableDropdownlist('#ddlCategory', '');
        $('#ulSearched').html('');
    },

    uploadImage: function (targetId) {
        var fileUpload = $(targetId).get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        $.ajax({
            type: "POST",
            url: "/Admin/Upload/UploadImage",
            contentType: false,
            processData: false,
            data: data,
            success: function (path) {
                $('#txtmdImage').val(path);
                $('#imgFood').prop('src', path);
                $('#imgFood').css('background-image', 'url(' + path + ')');
                common.notify('success', 'Upload image succesfully');

            },
            error: function () {
                common.notify('error', 'Can not load ajax function UploadImage');
            }
        });
    },

    getById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Food/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    food.resetAddEditModal();
                    var data = response.Data;
                    $('#hidId').val(data.Id);
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdCookingDuration').val(data.CookingDuration);
                    $('#ddlmdFoodCategory').val(data.FoodCategoryId);
                    $('#ddlmdUnit').val(data.Unit);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdFatPerUnit').val(data.FatPerUnit);
                    $('#txtmdSaturatedFatPerUnit').val(data.SaturatedFatPerUnit);
                    $('#txtmdCarbPerUnit').val(data.CarbPerUnit);
                    $('#txtmdProteinPerUnit').val(data.ProteinPerUnit);
                    $('#txtmdKcalPerUnit').val(data.KcalPerUnit);
                    $('#txtmdImage').val(data.Image);
                    $('#imgFood').css('background-image', 'url(' + data.Image + ')');
                    $('#grInput').prop('title', data.Image);
                    $('#mdlink').prop('href', data.CookingGuideLink);
                    $('#txtmdLink').val(data.CookingGuideLink);
                    $('#txtmdIngredient').val(data.Ingredient);
                    $('#txtmdRecipe').val(data.Recipe);
                    $('#txtmdTags').val(data.Tags);
                    $('#txtmdSeoPageTitle').val(data.SeoPageTitle);
                    $('#txtmdSeoAlias').val(data.SeoAlias);
                    $('#txtmdSeoKeywords').val(data.SeoKeywords);
                    $('#txtmdSeoDescription').val(data.SeoDescription);
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

    cloneFood: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/Food/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    food.resetAddEditModal();
                    var data = response.Data;
                    $('#hidId').val('0');
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdCookingDuration').val(data.CookingDuration);
                    $('#ddlmdFoodCategory').val(data.FoodCategoryId);
                    $('#ddlmdUnit').val(data.Unit);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdFatPerUnit').val(data.FatPerUnit);
                    $('#txtmdSaturatedFatPerUnit').val(data.SaturatedFatPerUnit);
                    $('#txtmdCarbPerUnit').val(data.CarbPerUnit);
                    $('#txtmdProteinPerUnit').val(data.ProteinPerUnit);
                    $('#txtmdKcalPerUnit').val(data.KcalPerUnit);
                    $('#txtmdImage').val(data.Image);
                    $('#imgFood').css('background-image', 'url(' + data.Image + ')');
                    $('#grInput').prop('title', data.Image);
                    $('#mdlink').prop('href', data.CookingGuideLink);
                    $('#txtmdLink').val(data.CookingGuideLink);
                    $('#txtmdIngredient').val(data.Ingredient);
                    $('#txtmdRecipe').val(data.Recipe);
                    $('#txtmdTags').val(data.Tags);
                    $('#txtmdSeoPageTitle').val(data.SeoPageTitle);
                    $('#txtmdSeoAlias').val(data.SeoAlias);
                    $('#txtmdSeoKeywords').val(data.SeoKeywords);
                    $('#txtmdSeoDescription').val(data.SeoDescription);
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

    resetAddEditModal: function () {
        $('#hidId').val('0');
        $('#txtmdCode').val('');
        $('#txtmdName').val('');
        $('#txtmdCookingDuration').val('');
        $('#ddlmdFoodCategory').val('');
        $('#ddlmdUnit').val('');
        $('#txtmdDescription').val('');
        $('#txtmdFatPerUnit').val('');
        $('#txtmdSaturatedFatPerUnit').val('');
        $('#txtmdCarbPerUnit').val('');
        $('#txtmdProteinPerUnit').val('');
        $('#txtmdKcalPerUnit').val('');
        $('#txtmdImage').val('');
        $('#imgFood').css('background-image', 'url("")');
        $('#mdlink').prop('href', '#');
        $('#txtmdLink').val('');
        $('#txtmdIngredient').val('');
        $('#txtmdRecipe').val('');
        $('#txtmdTags').val('');
        $('#txtmdSeoPageTitle').val('');
        $('#txtmdSeoAlias').val('');
        $('#txtmdSeoKeywords').val('');
        $('#txtmdSeoDescription').val('');
        $('#frmAddEdit').validate().resetForm();
    },

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/Food/SaveEntity",
                data: {
                    Id: $('#hidId').val(),
                    Active: true,
                    Code: $('#txtmdCode').val(),
                    Name: $('#txtmdName').val(),
                    CookingDuration: $('#txtmdCookingDuration').val(),
                    FoodCategoryId: $('#ddlmdFoodCategory').val(),
                    FoodCategoryCode: $('#ddlmdFoodCategory').find(':selected').data('code'),
                    Unit: $('#ddlmdUnit').val(),
                    Description: $('#txtmdDescription').val(),
                    FatPerUnit: $('#txtmdFatPerUnit').val(),
                    SaturatedFatPerUnit: $('#txtmdSaturatedFatPerUnit').val(),
                    CarbPerUnit: $('#txtmdCarbPerUnit').val(),
                    ProteinPerUnit: $('#txtmdProteinPerUnit').val(),
                    KcalPerUnit: $('#txtmdKcalPerUnit').val(),
                    CookingGuideLink: $('#txtmdLink').val(),
                    Ingredient: $('#txtmdIngredient').val(),
                    Recipe: $('#txtmdRecipe').val(),
                    Image: $('#txtmdImage').val(),
                    Tags: $('#txtmdTags').val(),
                    SeoPageTitle: $('#txtmdSeoPageTitle').val(),
                    SeoAlias: $('#txtmdSeoAlias').val(),
                    SeoKeywords: $('#txtmdSeoKeywords').val(),
                    SeoDescription: $('#txtmdSeoDescription').val()
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        food.getAllPaging(false);
                        food.resetAddEditModal();
                        if (id == 0) {
                            common.notify('success', 'Add Food successfully');
                        }
                        else {
                            common.notify('success', 'Update Food successfully');
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

    deleteFood: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Food/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        food.getAllPaging(true);
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