var dropdownlist = {

    getAllMealGroupedData: function (targetId) {
        $.ajax({
            type: 'GET',
            url: '/Admin/Meal/GetAllToGroupedData',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data != null) {
                        common.selectSearchWithData(targetId, response.Data);
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function (res) {
                common.notify('error', 'Can not load ajax function GetAllToGroupedData');
            }
        });
    },

    getAllMealGroupedData2: function (targetId) {
        $.ajax({
            type: 'GET',
            url: '/Admin/Meal/GetAllToGroupedData',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    var data = response.Data;
                    var render = '';
                    if (data != null) {
                        $.each(data, function (index, mother) {
                            render += '<optgroup label="' + mother.text + '">';
                            $.each(mother.children, function (index, child) {
                                render += '<option value="' + child.id + '" data-tokens="' + child.text + '">' + child.text + '</option>';
                            });
                        });
                        $(targetId).html(render);
                        $(targetId).selectpicker({
                            liveSearch: true,
                            size: 10,
                            style: 'form-control-sm btn-light',
                            selectedTextFormat: 'count > 6',
                            width: '100%',
                        });
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function (res) {
                common.notify('error', 'Can not load ajax function GetAllToGroupedData');
            }
        });
    },

    getAllBodyClassification: function (targetId) {
        $.ajax({
            type: 'GET',
            url: '/Admin/BodyClassification/GetAllToGroupedData',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    var data = response.Data;
                    var render = '';
                    if (data != null) {
                        $.each(data, function (index, mother) {
                            render += '<optgroup label="' + mother.text + '">';
                            $.each(mother.children, function (index, child) {
                                render += '<option value="' + child.id + '" data-tokens="' + child.text + '">' + child.text + '</option>';
                            });
                        });
                        $(targetId).html(render);
                        $(targetId).selectpicker({
                            liveSearch: true,
                            size: 10,
                            style: 'form-control-sm btn-light',
                            selectedTextFormat: 'count > 3',
                            width: '100%',
                        });
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function (res) {
                common.notify('error', 'Can not load ajax function GetAllToGroupedData');
            }
        });
    },

    getCategoriesByGroupCode: function (groupCode, targetId, selectedId) {
        return $.ajax({
            type: "GET",
            url: "/Admin/CommonCategory/GetByGroupCode",
            dataType: "json",
            data: {
                groupCode: groupCode
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

    // Begin Live search User
    getAllUser: function (targetId) {
        $(targetId).select2({
            ajax: {
                url: "/Admin/User/GetAllPaging",
                dataType: 'JSON',
                delay: 750,
                data: function (params) {
                    return {
                        roleName: '',
                        keyword: params.term,
                        page: params.page || 1,
                        pageSize: 10
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;

                    var select2Data = $.map(data.Data.Results, function (obj) {
                        obj.id = obj.Id;
                        obj.text = obj.Email;
                        return obj;
                    });

                    return {
                        results: select2Data,
                        pagination: {
                            more: (params.page * 10) < data.Data.RowCount
                        }
                    };
                },
                cache: true
            },
            theme: 'bootstrap4',
            placeholder: 'Search User',
            containerCssClass: ':all:',
            allowClear: true,
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 1,
            templateResult: dropdownlist.formatUser,
            templateSelection: dropdownlist.formatUserSelection
        });
    },

    formatUser: function (user) {
        if (user.loading) {
            return user.text;
        }

        var markup = "<div class='form-inline'>" +
            "<div><img class='img-responsive maxh-50' src='" + user.Avatar + "' /></div>" +
            "<div>" +
            "<h6>" + user.Email + "</h6>" +
            "<div>" + user.FullName || '' + "</div>" +
            "</div></div>";

        return markup;
    },

    formatUserSelection: function (user) {
        return user.text;
    },
    // End Live search User

    getAllTemplateMenu: function (targetId) {
        $.ajax({
            type: 'GET',
            url: '/Admin/TemplateMenu/GetAll',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    var data = response.Data;
                    var render = '';
                    if (data != null) {
                        $.each(data, function (index, templateMenu) {
                            render += '<option value="' + templateMenu.Id + '" data-subtext="' + templateMenu.Code + '" data-tokens="' + templateMenu.Code + '">' + templateMenu.Name + '</option>';
                        });
                        $(targetId).html(render);
                        $(targetId).selectpicker({
                            liveSearch: true,
                            size: 10,
                            style: 'form-control-sm btn-light',
                            selectedTextFormat: 'count > 3',
                            width: '100%',
                        });
                    }
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function (res) {
                common.notify('error', 'Can not load ajax function GetAllToGroupedData');
            }
        });
    },

}