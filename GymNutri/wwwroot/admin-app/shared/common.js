var common = {

    configs: {
        pageSize: 10,
        pageIndex: 1
    },

    notify: function (type, message) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        toastr[type](message);
    },

    popupNotify: function (_type, _confirmButtonText, _title, _text) {
        swal({
            type: _type,
            confirmButtonText: _confirmButtonText,
            title: _title,
            text: _text
        });
    },

    popupConfirm: function (_type, _title, _text, _okCallback) {
        swal({
            type: _type,
            title: _title,
            text: _text,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.value) {
                _okCallback();
            }
        })
    },

    startLoading: function (divId) {
        if ($(divId).length > 0)
            $(divId).prop('hidden', false);
    },

    stopLoading: function (divId) {
        if ($(divId).length > 0)
            $(divId).prop('hidden', true);
    },

    buttonStartLoading: function (id) {
        if ($(id).length > 0) {
            $(id).attr('disabled', 'disabled');
            var content = $(id).html();
            content = content + '  <i class="fa fa-refresh fa-spin"></i>';
            $(id).html(content);
        }
    },

    buttonStopLoading: function (id) {
        if ($(id).length > 0) {
            $(id).removeAttr('disabled');
            var content = $(id).html();
            content = content.replace('  <i class="fa fa-refresh fa-spin"></i>', '');
            $(id).html(content);
        }
    },

    sortTable: function (tableId, n) {
        var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
        table = document.getElementById(tableId);
        switching = true;
        // Set the sorting direction to ascending:
        dir = "asc";
        /* Make a loop that will continue until
        no switching has been done: */
        while (switching) {
            // Start by saying: no switching is done:
            switching = false;
            rows = table.rows;
            /* Loop through all table rows (except the
            first, which contains table headers): */
            for (i = 1; i < (rows.length - 1); i++) {
                // Start by saying there should be no switching:
                shouldSwitch = false;
                /* Get the two elements you want to compare,
                one from current row and one from the next: */
                x = rows[i].getElementsByTagName("TD")[n];
                y = rows[i + 1].getElementsByTagName("TD")[n];
                /* Check if the two rows should switch place,
                based on the direction, asc or desc: */
                if (dir == "asc") {
                    if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                        // If so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                } else if (dir == "desc") {
                    if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                        // If so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
            }
            if (shouldSwitch) {
                /* If a switch has been marked, make the switch
                and mark that a switch has been done: */
                rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                switching = true;
                // Each time a switch is done, increase this count by 1:
                switchcount++;
            } else {
                /* If no switching has been done AND the direction is "asc",
                set the direction to "desc" and run the while loop again. */
                if (switchcount == 0 && dir == "asc") {
                    dir = "desc";
                    switching = true;
                }
            }
        }
    },

    wrapPaging: function (targetId, recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / common.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($(targetId + ' a').length === 0 || changePageSize === true) {
            $(targetId).empty();
            $(targetId).removeData("twbs-pagination");
            $(targetId).unbind("page");
        }
        //Bind Pagination Event
        $(targetId).twbsPagination({
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
    },

    dateFormatJson: function (datetime) {
        if (datetime == null || datetime == '')
            return '';
        var newdate = new Date(datetime);
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        var hh = newdate.getHours();
        var mm = newdate.getMinutes();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        return day + "/" + month + "/" + year;
    },

    dateTimeFormatJson: function (datetime) {
        if (datetime == null || datetime == '')
            return '';
        var newdate = new Date(datetime);
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        var hh = newdate.getHours();
        var mm = newdate.getMinutes();
        var ss = newdate.getSeconds();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        if (ss < 10)
            ss = "0" + ss;
        return day + "/" + month + "/" + year + " " + hh + ":" + mm + ":" + ss;
    },

    formatNumber: function (number, precision) {
        if (!isFinite(number)) {
            return number.toString();
        }

        var a = number.toFixed(precision).split('.');
        a[0] = a[0].replace(/\d(?=(\d{3})+$)/g, '$&,');
        return a.join('.');
    },

    setupDatePicker: function (id, dateFormat) {
        $(id).datepicker({
            format: dateFormat,
            orientation: "bottom right",
            autoclose: true,
            todayHighlight: true
        });
    },

    setupDatePickerMonth: function (id, dateFormat) {
        $(id).datepicker({
            format: dateFormat,
            orientation: "bottom right",
            autoclose: true,
            todayHighlight: true,
            startView: "months",
            minViewMode: "months"
        });
    },

    selectSearch: function (id) {
        $(id).select2({
            theme: 'bootstrap4',
            containerCssClass: ':all:',
            placeholder: "-- Select --",
            allowClear: true,
            width: '100%'
        });
    },

    selectSearchWithData: function (id, data) {
        $(id).select2({
            theme: 'bootstrap4',
            containerCssClass: ':all:',
            placeholder: "-- Select --",
            allowClear: true,
            width: '100%',
            data: data
        });
    },

    setValueSearchableDropdownlist: function (id, value) {
        $(id).val(value);
        $(id).trigger('change');
    },

    diffMonths: function (dt1, dt2) {
        var diff = (dt2.getTime() - dt1.getTime()) / 1000;
        diff /= (60 * 60 * 24 * 7 * 4);
        return Math.abs(Math.round(diff));
    },

    //formatNumber: function (nStr) {
    //    nStr += '';
    //    x = nStr.split('.');
    //    x1 = x[0];
    //    x2 = x.length > 1 ? '.' + x[1] : '';
    //    var rgx = /(\d+)(\d{3})/;
    //    while (rgx.test(x1)) {
    //        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    //    }
    //    return x1 + x2;
    //},

    formatComma: function (str) {
        str += '';
        str = str.replace(',', ''); str = str.replace(',', ''); str = str.replace(',', '');
        str = str.replace(',', ''); str = str.replace(',', ''); str = str.replace(',', '');
        x = str.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1))
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        return x1 + x2;
    },

    isNumberKey: function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode == 46) // decimal point key
            return true;

        if (charCode > 31 && (charCode < 48 || charCode > 57)) // number key
            return false;
        return true;
    },

    unflattern: function (arr) {
        var map = {};
        var roots = [];
        for (var i = 0; i < arr.length; i += 1) {
            var node = arr[i];
            node.children = [];
            map[node.id] = i; // use map to look-up the parents
            if (node.parentId !== null) {
                arr[map[node.parentId]].children.push(node);
            } else {
                roots.push(node);
            }
        }
        return roots;
    }
}

$(document).ajaxSend(function (e, xhr, options) {
    if (options.type.toUpperCase() == "POST" || options.type.toUpperCase() == "PUT") {
        var token = $('form').find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});