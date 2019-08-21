var HomeController = function () {
    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {
        $('#btnDemo').off('click').on('click', function (e) {
            //common.popupNotify("Good job!", "You clicked the button!", "success", "OK");
            common.popupConfirm("Are you sure to delete ?",
                "You will not be able to recover this imaginary file !!",
                "warning",
                function () {
                    loadData('01/01/2018', '12/12/2018');
                }
            )
        });
    }

    function loadData(from, to) {
        $.ajax({
            type: "GET",
            url: "/Admin/Home/GetRevenue",
            data: {
                fromDate: from,
                toDate: to
            },
            dataType: "json",
            beforeSend: function () {
                common.startLoading();
            },
            success: function (response) {
                initChart(response);
                common.stopLoading();
            },
            error: function (status) {
                common.notify('Có lỗi xảy ra', 'error');
                common.stopLoading();
            }
        });
    }
    
}