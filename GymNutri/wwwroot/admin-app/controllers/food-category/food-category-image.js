var foodCategoryImage = {

    initialize: function () {
        foodCategoryImage.registerControls();
        foodCategoryImage.registerEvents();
    },

    registerEvents: function () {
        $('body').on('click', '.btn-images', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $('#hidFoodCategoryId').val(id);
            $("#fileImage").val('');
            foodCategoryImage.getImages();
            $('#images-modal').modal('show');
        });

        $('body').on('click', '.btn-delete-image', function (e) {
            e.preventDefault();
            $(this).closest('div').remove();
        });

        $("#btnSaveImages").on('click', function () {
            foodCategoryImage.saveImages();
        });

        $("#fileImage").on('change', function () {
            foodCategoryImage.uploadImage(this);
        });

        $('body').on('click', '.btn-delete-image', function (e) {
            
        });
    },

    registerControls: function () {

    },

    saveImages: function () {
        var imageList = [];
        $.each($('#image-list').find('img'), function (i, item) {
            imageList.push($(this).data('path'));
        });

        $.ajax({
            url: '/Admin/FoodCategory/SaveImages',
            data: {
                foodCategoryId: $('#hidFoodCategoryId').val(),
                images: imageList
            },
            beforeSend: function () {
                common.buttonStartLoading('#btnSaveImages');
            },
            type: 'post',
            dataType: 'json',
            success: function (response) {
                if (response.Success == true) {
                    $('#images-modal').modal('hide');
                    $('#image-list').html('');
                    $("#fileImage").val('');
                    common.notify('success', 'Upload images successfully');
                }
                else {
                    common.notify('error', response.Message);
                }
                common.buttonStopLoading('#btnSaveImages');
            },
            error: function () {
                common.buttonStopLoading('#btnSaveImages');
                common.notify('error', 'Can not load ajax function SaveImages');
            }
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
                $("#fileImage").val('');
                $('#image-list').append('<div class="col-md-3"><img class="img-responsive" data-path="' + path + '" src="' + path + '"><br/><a href="#" class="btn-delete-image">Delete</a></div>');
                common.notify('success', 'Upload image successfully');
            },
            error: function () {
                common.notify('error', 'Can not load ajax function UploadImage');
            }
        });
    },

    getImages: function () {
        $.ajax({
            url: '/Admin/FoodCategory/GetImages',
            data: {
                foodCategoryId: $('#hidFoodCategoryId').val()
            },
            type: 'get',
            dataType: 'json',
            success: function (response) {
                var render = '';
                $.each(response.Data, function (i, item) {
                    render += '<div class="col-md-3"><img class="img-responsive" data-path="' + item.Path + '" src="' + item.Path + '"><br/><a href="#" class="btn-delete-image">Delete</a></div>';
                });
                $('#image-list').html(render);
                $("#fileImage").val('');
            }
        });
    }
 
}