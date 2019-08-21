var customer = {

    initialize: function () {
        customer.getAllPaging(true);
        customer.registerControls();
        customer.registerEvents();
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
            customer.getAllPaging(true);
        });
        
        $('#ddlSortBy').on('change', function () {
            customer.getAllPaging(false);
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
            customer.getAllPaging(true);
        });
        
        $('#btnErase').off('click').on('click', function (e) {
            customer.resetFilters();
            customer.getAllPaging(true);
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                customer.getAllPaging(true);
            }
        });

        $('body').on('click', '.sortable', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            common.sortTable('tbl-customer', id);
        });
        
        $('body').on('click', '.btn-body-index', function (e) {
            e.preventDefault();
            var userId = $(this).data('id');
            $('#rDetailInfor').hide();
            $('#btnSaveBodyIndex').prop('hidden', true);
            $('#btnCancelBodyIndex').prop('hidden', true);
            $('#btnCreateBodyIndex').prop('hidden', false);
            $('#txtFromDate').val('');
            $('#txtToDate').val('');
            bodyIndex.getAllPagingBodyIndex(userId, true);
        });

        $('body').on('click', '.btn-location', function (e) {
            e.preventDefault();
            var userId = $(this).data('id');
            $('#frmLocation').hide();
            $('#btnSaveLocation').prop('hidden', true);
            $('#btnCancelLocation').prop('hidden', true);
            $('#btnCreateLocation').prop('hidden', false);
            userLocation.getAllPagingLocation(userId, true);
            $('#location-modal').modal('show');
        });

        $('body').on('click', '.btn-desire', function (e) {
            e.preventDefault();
            var userId = $(this).data('id');
            $('#frmDesire').hide();
            $('#btnSaveDesire').prop('hidden', true);
            $('#btnCancelDesire').prop('hidden', true);
            $('#btnCreateDesire').prop('hidden', false);
            userDesire.getAllPagingDesire(userId, true);
            $('#desire-modal').modal('show');

        });
        

    },

    registerControls: function () {
        $(".slider").bootstrapSlider();
        common.setupDatePicker('.datepicker', 'dd/mm/yyyy');
    },

    getAllPaging: function (isPageChanged) {
        var template = $('#template-customer').html();
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
            url: '/Admin/Customer/GetAllPaging',
            dataType: 'JSON',
            success: function (response) {
                if (response.Success == true) {
                    if (response.Data.RowCount > 0) {
                        $.each(response.Data.Results, function (i, item) {
                            render += Mustache.render(template, {
                                Id: item.Id,
                                UserName: item.UserName,
                                Email: item.Email,
                                FullName: item.FullName,
                                PhoneNumber: item.PhoneNumber,
                                DateCreated: common.dateTimeFormatJson(item.DateCreated)
                            });
                        });

                        if (render != '') {
                            $('#tbody-customer').html(render);
                        }

                        common.wrapPaging('#paginationUL', response.Data.RowCount, function () {
                            customer.getAllPaging(false);
                        }, isPageChanged);

                        $('#lblTotalRecords').text(response.Data.RowCount);
                    }
                    else {
                        render = '<td colspan=7><b>There is no data</b></td>';
                        $('#tbody-customer').html(render);
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
        $('#ulSearched').html('');
    },

}