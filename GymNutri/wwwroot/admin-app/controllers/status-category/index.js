var StatusCategoryController = function () {
    var cachedObj = {
        tables: []
    }

    this.initialize = function () {
        getAllPaging(false);

        $.when(getAllTable()).then(function () {
            registerControls();
        });
        
        registerEvents();
    }

    function registerEvents() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                ddlmdTable: { required: true },
                txtmdCode: { required: true },
                txtmdName: { required: true },
                txtmdColor: { required: true },
                txtmdOrder: { required: true }
            }
        });

        $('#ddlShowPage').on('change', function () {
            common.configs.pageSize = $(this).val();
            common.configs.pageIndex = 1;
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

        $('#ddlTable').on('change', function () {
            getAllPaging(true);
            var selected = $(this).val();
            if (selected.length > 0) {
                $('#liTable').remove();
                $("#ulSearched").append('<li class="list-inline-item font-italic" id="liTable">Table: <b>' + selected + '</b></li>');
            }
            else {
                $('#liTable').remove();
            }
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                getAllPaging(true);
            }
        });

        $('#btnSearch').off('click').on('click', function (e) {
            getAllPaging(true);
        });

        $('#btnErase').off('click').on('click', function (e) {
            resetFilters();
            getAllPaging(true);
        });

        $('#btnCreate').off('click').on('click', function (e) {
            resetAddEditModal();
            $('#spAddEdit').text('Add');
            $('#add-edit-modal').modal('show');
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            getById(id);
        });

        $('body').on('click', '.btn-clone', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            cloneStatusCategory(id);
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-status-category', id);
        });
        
        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            deleteStatusCategory(id);
        });

        $('#btnSave').on('click', function (e) {
            saveEntity(e);
        });
        
    }

    function registerControls() {
        $('#ddlTable').html(initOptionTable(0));
        $('#ddlmdTable').html(initOptionTable(0));
    }
    
    function getAllPaging(isPageChanged) {
        var template = $('#tbody-status-category-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                table: $('#ddlTable').val() == '-- Select table --' ? '' : $('#ddlTable').val(),
                keyword: $('#txtKeyword').val(),
                sortBy: $('#ddlSortBy').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            url: '/Admin/StatusCategory/GetAllPaging',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                Table: item.Table,
                                OrderNo: item.OrderNo,
                                Code: item.Code,
                                Name: item.Name,
                                Description: item.Description,
                                Color: '<span class="badge badge-pill badge-success" style="background-color: ' + item.Color + '">' + item.Color + '</span>',
                                ShowInAdmin: item.ShowInAdmin,
                                ShowInClient: item.ShowInClient
                            });
                        });
                        
                        if (render != '') {
                            $('#tbody-status-category').html(render);
                        }
                        wrapPaging(response.Data.RowCount, function () {
                            getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=10 style=""><b>There is no data</b></td>';
                        $('#tbody-status-category').html(render);
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
    }

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

    function deleteStatusCategory(id) {
        common.popupConfirm('warning', 'Delete', 'Are you sure to delete this record?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/StatusCategory/Delete",
                data: { id: id },
                dataType: "JSON",
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
            url: "/Admin/StatusCategory/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    $('#frmMaintainance').validate().resetForm();
                    var data = response.Data;
                    $('#hidId').val(data.Id);
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdOrder').val(data.OrderNo);
                    $('#ddlmdTable').val(data.Table);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdColor').val(data.Color);
                    $('#ckmdShowInAdmin').iCheck(data.ShowInAdmin == true ? 'check' : 'uncheck');
                    $('#ckmdShowInClient').iCheck(data.ShowInClient == true ? 'check' : 'uncheck');
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
    }

    function cloneStatusCategory(id) {
        $.ajax({
            type: "GET",
            url: "/Admin/StatusCategory/GetById",
            data: { id: id },
            dataType: "JSON",
            beforeSend: function () {
                common.startLoading('#preloader');
            },
            success: function (response) {
                if (response.Success == true) {
                    $('#frmMaintainance').validate().resetForm();
                    var data = response.Data;
                    $('#hidId').val('0');
                    $('#txtmdCode').val(data.Code);
                    $('#txtmdName').val(data.Name);
                    $('#txtmdOrder').val(data.OrderNo);
                    $('#ddlmdTable').val(data.Table);
                    $('#txtmdDescription').val(data.Description);
                    $('#txtmdColor').val(data.Color);
                    $('#ckmdShowInAdmin').iCheck(data.ShowInAdmin == true ? 'check' : 'uncheck');
                    $('#ckmdShowInClient').iCheck(data.ShowInClient == true ? 'check' : 'uncheck');
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

    function getAllTable() {
        return $.ajax({
            type: "GET",
            url: "/Admin/StatusCategory/GetAllTable",
            dataType: "json",
            success: function (response) {
                if (response.Success == true) {
                    cachedObj.tables = response.Data;
                }
                else {
                    common.notify('error', response.Message);
                }
            },
            error: function () {
                common.notify('error', 'Can not load ajax function GetAllTable');
            }
        });
    }

    function initOptionTable(selectedId) {
        var tables = "<option value=''>-- Select table --</option>";
        $.each(cachedObj.tables, function (i, table) {
            if (selectedId === table.Table)
                tables += '<option value="' + table.Table + '" selected>' + table.Table + '</option>';
            else
                tables += '<option value="' + table.Table + '">' + table.Table + '</option>';
        });
        return tables;
    }

    function saveEntity(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidId').val();
            var code = $('#txtmdCode').val();
            var name = $('#txtmdName').val();
            var orderNo = $('#txtmdOrder').val();
            var Table = $('#ddlmdTable').val();
            var description = $('#txtmdDescription').val();
            var color = $('#txtmdColor').val();
            var showInAdmin = $('#ckmdShowInAdmin').prop('checked');
            var showInClient = $('#ckmdShowInClient').prop('checked');
            
            $.ajax({
                type: "POST",
                url: "/Admin/StatusCategory/SaveEntity",
                data: {
                    Id: id,
                    Code: code,
                    Name: name,
                    OrderNo: orderNo,
                    Table: Table,
                    Description: description,
                    Color: color,
                    ShowInAdmin: showInAdmin,
                    ShowInClient: showInClient,
                    Active: true
                },
                dataType: "json",
                beforeSend: function () {
                    common.buttonStartLoading('#btnSave');
                },
                success: function (response) {
                    if (response.Success) {
                        $('#add-edit-modal').modal('hide');
                        getAllPaging(false);
                        resetAddEditModal();
                        if (id == 0) {
                            common.notify('success', 'Add Status Category successfully');
                        }
                        else {
                            common.notify('success', 'Update Status Category successfully');
                        }
                    }
                    else {
                        var errorList = '';
                        $.each(response.Data, function (i, item) {
                            errorList += item.ErrorMessage + ' \n';
                        });
                        common.notify('error', errorList);
                    }
                    common.buttonStopLoading('#btnSave');
                },
                error: function () {
                    common.notify('error', 'Can not load ajax function SaveEntity');
                    common.buttonStopLoading('#btnSave');
                }
            });
            
        }
    }

    function resetAddEditModal() {
        $('#hidId').val('0');
        $('#txtmdCode').val('');
        $('#txtmdName').val('');
        $('#txtmdOrder').val('');
        $('#ddlmdTable').val('');
        $('#txtmdDescription').val('');
        $('#txtmdColor').val('');
        $('#ckmdShowInAdmin').prop('checked', false);
        $('#ckmdShowInClient').prop('checked', false);
        $('#frmMaintainance').validate().resetForm();
    }

    function resetFilters () {
        $('#txtKeyword').val('');
        $('#ddlSortBy').val('');
        $('#ddlTable').val('');
        $('#ulSearched').html('');
    }

}