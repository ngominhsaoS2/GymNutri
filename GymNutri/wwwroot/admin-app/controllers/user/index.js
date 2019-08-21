var UserController = function () {
    this.initialize = function () {
        getAllPaging(true);
        registerControls();
        registerEvents();
    }

    function registerEvents() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtmdUserName: { required: true },
                txtmdPassword: {
                    required: true,
                    minlength: 6
                },
                txtmdConfirmPassword: {
                    equalTo: "#txtmdPassword"
                },
                txtmdFullName: { required: true },
                txtmdEmail: {
                    required: true,
                    email: true
                }
            }
        });

        $('#txtKeyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                getAllPaging(true);
            }
        });

        $("#btnSearch").on('click', function () {
            getAllPaging(true);
        });

        $('#btnErase').off('click').on('click', function (e) {
            resetFilters();
            getAllPaging(true);
        });

        $('#ddlSortBy').on('change', function () {
            getAllPaging(false);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liSortBy').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liSortBy">Sort: <b>' + selected + '</b></li>');
            }
            else {
                $('#liSortBy').remove();
            }
        });

        $('#ddlRole').on('change', function () {
            getAllPaging(true);
            var selected = $("#ddlRole option:selected").text();
            if (selected.length > 0) {
                $('#liRole').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liRole">Role: <b>' + selected + '</b></li>');
            }
            else {
                $('#liRole').remove();
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
            getAllPaging(true);
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-user', id);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $('#spAddEdit').text('Edit');
            getById(id);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            deleteUser(id);
        });

        $('#btnSave').on('click', function (e) {
            saveEntity(e);
        });

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });

        $("#fileInputImage").on('change', function () {
            uploadImage(this);
        });

    }

    function registerControls() {
        initRoleList();
        common.setupDatePicker('.datepicker', 'dd/mm/yyyy');
    }

    function disableFieldEdit(disabled) {
        $('#txtmdUserName').prop('disabled', disabled);
        $('#txtmdPassword').prop('disabled', disabled);
        $('#txtmdConfirmPassword').prop('disabled', disabled);
    }

    function resetAddEditModal() {
        disableFieldEdit(false);
        $('#hidId').val('');
        //initRoleList();
        $('#txtmdFullName').val('');
        $('#txtmdUserName').val('');
        $('#txtmdPasswordtxtmdPassword').val('');
        $('#txtmdConfirmPassword').val('');
        $('input[name="ckRoles"]').removeAttr('checked');
        $('#txtmdEmail').val('');
        $('#txtmdPhoneNumber').val('');
        $('#txtmdBirthday').val('');
        $('#txtmdImage').val('');
        $('#imgUser').css('background-image', 'url("")');
        $('#ckMale').iCheck('uncheck');
        $('#ckFemale').iCheck('uncheck');
        $('#ckActive').iCheck('check');
        $('#frmMaintainance').validate().resetForm();
    }

    function initRoleList(selectedRoles) {
        $.ajax({
            url: "/Admin/Role/GetAll",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var template = $('#role-template').html();
                var data = response.Data; 
                var renderListRoles = '';
                var renderDropDownList = '';

                $.each(data, function (i, item) {
                    // 25/11/2018 SaoNM Initialize các checkbox theo Roles
                    var checked, selected;
                    if (selectedRoles !== undefined && selectedRoles.indexOf(item.Name) !== -1) {
                        checked = 'checked';
                        selected = 'selected';
                    }
                        
                    renderListRoles += Mustache.render(template,
                        {
                            Name: item.Name,
                            Checked: checked
                        });

                    // 25/11/2018 SaoNM Initialize dropdownlist tìm kiếm theo Role
                    renderDropDownList += '<option value="' + item.Id + '" ' + selected + '>' + item.Name + '</option>';
                });

                $('#list-roles').html(renderListRoles);

                $('#ddlRole').html("<option value=''>-- Select Role --</option>" + renderDropDownList);

                //Thêm theme iCheck
                $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                    checkboxClass: 'icheckbox_flat-green',
                    radioClass: 'iradio_flat-green'
                });
            }
        });
    }

    function getAllPaging(isPageChanged) {
        var template = $('#tbody-user-template').html();
        var render = "";

        var role = $("#ddlRole option:selected").text();
        $.ajax({
            type: "GET",
            url: "/Admin/User/GetAllPaging",
            data: {
                roleName: role.indexOf('Select') > 0 ? '' : role,
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            dataType: "json",
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                UserName: item.UserName,
                                Email: item.Email,
                                FullName: item.FullName,
                                DateCreated: common.dateTimeFormatJson(item.DateCreated)
                            });
                        });
                        
                        if (render != '') {
                            $('#tbody-user').html(render);
                        }
                        wrapPaging(response.Data.RowCount, function () {
                            getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=6><b>There is no data</b></td>';
                        $('#tbody-user').html(render);
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
    };

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / common.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: '<<',
            prev: '<',
            next: '>',
            last: '>>',
            onPageClick: function (event, p) {
                if (common.configs.pageIndex !== p) {
                    common.configs.pageIndex = p;
                    setTimeout(callBack(), 200);
                }
            }
        });
    }

    function deleteUser(id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/User/Delete",
                data: { id: id },
                success: function (response) {
                    if (response.Success == true) {
                        common.notify('success', 'Delete successfully');
                        getAllPaging(true);
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
    }

    function getById(id) {
        $.ajax({
            type: "GET",
            url: "/Admin/User/GetById",
            data: { id: id },
            dataType: "JSON",
            success: function (response) {
                if (response.Success == true) {
                    var data = response.Data;
                    $('#frmMaintainance').validate().resetForm();
                    $('#hidId').val(data.Id);
                    $('#txtmdFullName').val(data.FullName);
                    $('#txtmdUserName').val(data.UserName);
                    $('#txtmdEmail').val(data.Email);
                    $('#txtmdPhoneNumber').val(data.PhoneNumber);
                    $('#txtmdBirthday').val(common.dateFormatJson(data.Birthday));
                    $('#ckMale').iCheck(data.Gender == 'male' ? 'check' : 'uncheck');
                    $('#ckFemale').iCheck(data.Gender == 'female' ? 'check' : 'uncheck');
                    $('#ckActive').iCheck(data.Active == true ? 'check' : 'uncheck');
                    $('#txtmdImage').val(data.Avatar);
                    $('#imgUser').css('background-image', 'url(' + data.Avatar + ')');
                    initRoleList(data.Roles);
                    disableFieldEdit(true);
                    $('#add-edit-modal').modal('show');
                }
            },
            error: function () {
                common.notify('error', 'Can not load ajax function GetById');
            }
        });
    }

    function saveEntity(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();
            var fullName = $('#txtmdFullName').val();
            var userName = $('#txtmdUserName').val();
            var password = $('#txtmdPassword').val();
            var email = $('#txtmdEmail').val();
            var phoneNumber = $('#txtmdPhoneNumber').val();
            var birthday = $('#txtmdBirthday').val();
            var roles = [];
            $.each($('input[name="ckRoles"]'), function (i, item) {
                if ($(item).prop('checked') === true)
                    roles.push($(item).prop('value'));
            });
            var active = $('#ckActive').prop('checked') === true ? true : false;
            var gender = $('#ckFemale').prop('checked') === true ? 'female' : 'male';

            $.ajax({
                type: "POST",
                url: "/Admin/User/SaveEntity",
                data: {
                    Id: id,
                    FullName: fullName,
                    UserName: userName,
                    Password: password,
                    Email: email,
                    PhoneNumber: phoneNumber,
                    Birthday: birthday,
                    Avatar: $('#txtmdImage').val(),
                    Gender: gender,
                    Active: active,
                    Roles: roles
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    $('#add-edit-modal').modal('hide');
                    resetAddEditModal();
                    common.buttonStopLoading('#btnSave');
                    common.notify('success', 'Save User succesfully');
                    getAllPaging(true);
                },
                error: function () {
                    common.buttonStopLoading('#btnSave');
                    common.notify('error', 'Can not load ajax function SaveEntity');
                }
            });
        }
    }

    function resetFilters () {
        $('#txtKeyword').val('');
        $('#ddlSortBy').val('');
        $('#ddlRole').val('');
        $('#ulSearched').html('');
    }

    function uploadImage (targetId) {
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
                $('#imgUser').prop('src', path);
                $('#imgUser').css('background-image', 'url(' + path + ')');
                common.notify('success', 'Upload image succesfully');

            },
            error: function () {
                common.notify('error', 'Can not load ajax function UploadImage');
            }
        });
    }


}