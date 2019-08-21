// Set iCheck for all pages
$('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
    checkboxClass: 'icheckbox_flat-green',
    radioClass: 'iradio_flat-green'
});

$('.number-comma').on('keyup', function (e) {
    var number = common.formatComma(this.value);
    this.value = number;
});

$('.number').on('keypress', function (e) {
    return common.isNumberKey(e);
});

// Bổ sung method lessDate cho thư viện jquery validation
// So sánh ngày nhập vào có nhỏ hơn hoặc bằng ngày so sánh không, nhỏ hơn hoặc bằng thì trả về true, ngược lại thì trả về false
$.validator.addMethod("lessDate", function (value, element, params) {
    // Ngày nhập vào
    var arr = value.split("/");
    var inputDay = new Date(arr[2], arr[1] - 1, arr[0]);

    // So sánh và trả về kết quả
    if (arr.length > 0) {
        // Ngày so sánh
        arr = $(params[0]).val().split("/");
        var compareDay = new Date(arr[2], arr[1] - 1, arr[0]);

        if (inputDay <= compareDay)
            return true;
        return false;
    }
    else {
        return true;
    }
       
}, $.validator.format('Must be less than EndDate (ToDate)'));

// Bổ sung method greateDate cho thư viện jquery validation
// So sánh ngày nhập vào có lớn hơn hoặc bằng ngày so sánh không, lớn hơn hoặc bằng thì trả về true, ngược lại thì trả về false
$.validator.addMethod("greateDate", function (value, element, params) {
    // Ngày nhập vào
    var arr = value.split("/");
    var inputDay = new Date(arr[2], arr[1] - 1, arr[0]);

    // So sánh và trả về kết quả
    if (arr.length > 0) {
        // Ngày so sánh
        arr = $(params[0]).val().split("/");
        var compareDay = new Date(arr[2], arr[1] - 1, arr[0]);

        if (inputDay >= compareDay)
            return true;
        return false;
    }
    else {
        return true;
    }

}, $.validator.format('Must be great than StartDate (FromDate)'));

// Bổ sung method checkFileExtension cho thư viện jquery validation
$.validator.addMethod("checkFileExtension", function (value, element, params) {
    var ext = value.split('.').pop();
    if (params[0].indexOf(ext) > -1) {
        return true;
    }
    else {
        return false; // Hiển thị thông báo lỗi
    }

}, $.validator.format('Select file has extension: {0}'));

// Bổ sung method minWithCommasNumber cho thư viện jquery validation
// Tương tự như method min có sẵn của thư viện, chỉ khác là đầu vào là chuỗi text có dấu phẩy (,) phân cách. Ví dụ 1,000,0000 thay vì 1000000
$.validator.addMethod("minWithCommasNumber", function (value, element, params) {
    // Giá trị nhập vào
    var input = value.replace(/,/g, "");

    // So sánh và trả về kết quả
    if (input < params[0]) {
        return false; // Hiển thị thông báo lỗi
    }
    else {
        return true;
    }

}, $.validator.format('Giá trị nhập vào phải lớn hơn {0}'));

