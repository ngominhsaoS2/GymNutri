var foodCategory = {

    initialize: function () {
        foodCategory.getAllPaging(true);
        foodCategory.registerControls();
        foodCategory.registerEvents();
    },

    registerEvents: function () {
        $('#frmAddEdit').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdCode: { required: true },
                txtmdName: { required: true },
                ddlmdFoodTypeCode: { required: true },
                txtmdOrderNo: { required: true }
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            foodCategory.getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            foodCategory.getAllPaging(false);
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
            foodCategory.getAllPaging(true);
        });

        $('#btnErase').off('click').on('click', function (e) {
            foodCategory.resetFilters();
            foodCategory.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                foodCategory.getAllPaging(true);
            }
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-food-category', id);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            foodCategory.resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('#btnSave').on('click', function (e) {
            foodCategory.saveEntity(e);
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            foodCategory.getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            foodCategory.cloneFoodCategory(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            foodCategory.deleteFoodCategory(id);
        });

        $('body').on('click', '.btn-images', function (e) {
            $('#images-modal').modal('show');
        });

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });

        $("#fileInputImage").on('change', function () {
            foodCategory.uploadImage(this);
        });

    },

    registerControls: function () {
        foodCategory.getByGroupCode('#ddlmdFoodTypeCode', '');
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#tbody-food-category-template').html();
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
            url: '/Admin/FoodCategory/GetAllPaging',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                ParentId: item.ParentId,
                                Code: item.Code,
                                Name: item.Name,
                                FoodTypeCode: item.FoodTypeCode,
                                Description: item.Description,
                                OrderNo: item.OrderNo,
                                Image: item.Image
                            });
                        });

                        if (render != '') {
                            $('#tbody-food-category').html(render);
                        }

                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            foodCategory.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=7><b>There is no data</b></td>';
                        $('#tbody-food-category').html(render);
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

    getById: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/FoodCategory/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    foodCategory.resetAddEditModal();
                    var data = response.Data;
                    $('#hidId').val(data.Id);
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdOrderNo').val(data.OrderNo);
                    $('#ddlmdFoodTypeCode').val(data.FoodTypeCode);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdImage').val(data.Image);
                    //$('#imgFoodCategory').prop('src', data.Image);
                    $('#imgFoodCategory').css('background-image', 'url(' + data.Image + ')');
                    $('#grInput').prop('title', data.Image);
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

    cloneFoodCategory: function (id) {
        $.ajax({
            type: "GET",
            url: "/Admin/FoodCategory/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    foodCategory.resetAddEditModal();
                    var data = response.Data;
                    $('#hidId').val('0');
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdOrderNo').val(data.OrderNo);
                    $('#ddlmdFoodTypeCode').val(data.FoodTypeCode);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdImage').val(data.Image);
                    //$('#imgFoodCategory').prop('src', data.Image);
                    $('#imgFoodCategory').css('background-image', 'url(' + data.Image + ')');
                    $('#grInput').prop('title', data.Image);
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
        $('#txtmdOrderNo').val('');
        $('#ddlmdFoodTypeCode').val('');
        $('#txtmdDescription').val('');
        $('#txtmdImage').val('');
        //$('#imgFoodCategory').prop('src', '');
        $('#imgFoodCategory').css('background-image', 'url("")');
        $('#txtmdTags').val('');
        $('#txtmdSeoPageTitle').val('');
        $('#txtmdSeoAlias').val('');
        $('#txtmdSeoKeywords').val('');
        $('#txtmdSeoDescription').val('');
        $('#frmAddEdit').validate().resetForm();
    },

    getByGroupCode: function (targetId, selectedId) {
        return $.ajax({
            type: "GET",
            url: "/Admin/CommonCategory/GetByGroupCode",
            dataType: "json",
            data: {
                groupCode: constants.groupcode.FoodType
            },
            success: function (response) {
                if (response.Success == true) {
                    var render = "<option value=''>-- Select --</option>";
                    $.each(response.Data, function (i, item) {
                        if (selectedId === item.Code)
                            render += '<option value="' + item.Code + '" selected>' + item.Code + ' - ' + item.Name + '</option>';
                        else
                            render += '<option value="' + item.Code + '">' + item.Code + ' - ' + item.Name + '</option>';
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

    saveEntity: function (e) {
        if ($('#frmAddEdit').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/FoodCategory/SaveEntity",
                data: {
                    Id: $('#hidId').val(),
                    Active: true,
                    //ParentId: '',
                    Code: $('#txtmdCode').val(),
                    Name: $('#txtmdName').val(),
                    FoodTypeCode: $('#ddlmdFoodTypeCode').val(),
                    Description: $('#txtmdDescription').val(),
                    OrderNo: $('#txtmdOrderNo').val(),
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
                        foodCategory.getAllPaging(false);
                        foodCategory.resetAddEditModal();
                        if (id == 0) {
                            common.notify('success', 'Add Food Category successfully');
                        }
                        else {
                            common.notify('success', 'Update Food Category successfully');
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

    deleteFoodCategory: function (id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/FoodCategory/Delete",
                data: { id: id },
                dataType: "JSON",
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        foodCategory.getAllPaging(true);
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
                $('#imgFoodCategory').prop('src', path);
                $('#imgFoodCategory').css('background-image', 'url(' + path + ')');
                common.notify('success', 'Upload image succesfully');

            },
            error: function () {
                common.notify('error', 'Can not load ajax function UploadImage');
            }
        });
    },

    resetFilters: function () {
        $('#txtKeyword').val('');
        $('#ddlSortBy').val('');
        $('#ulSearched').html('');
    },

}