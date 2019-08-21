var LoginController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {
        $('#frmLogin').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtUserName: {
                    required: true
                },
                txtPassword: {
                    required: true
                }
            }
        });

        $('#btnLogin').off('click').on('click', function (e) {
            if ($('#frmLogin').valid()) {
                e.preventDefault();
                var user = $('#txtUserName').val();
                var pass = $('#txtPassword').val();
                login(user, pass);
            }
        });

        $('.login-control').on('keypress', function (e) {
            if (e.which === 13) {
                var user = $('#txtUserName').val();
                var pass = $('#txtPassword').val();
                login(user, pass);
            }
        });
    }

    var login = function (user, pass) {
        $.ajax({
            type: 'POST',
            data: {
                UserName: user,
                Password: pass
            },
            beforeSend: function () {
                common.buttonStartLoading('#btnLogin');
            },
            dataType: 'JSON',
            url: '/Admin/Login/Authen',
            success: function (res) {
                if (res.Success) {
                    window.location.href = "/Admin/Home/Index";
                }
                else {
                    common.notify('error', res.Message);
                    common.buttonStopLoading('#btnLogin');
                }
            }
        })
    }

}