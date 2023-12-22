function Login(id, pass) {
    var coder = id.toString();
    $.ajax({
        url: "Account/GetAccount",
        type: 'post',
        data: {
            code: coder,
            password: pass
        },
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Mã/Mật khẩu không đúng')
            } else {
                localStorage.setItem('accessToken', response.token);

                var redirectRequest = new Request(response.url, {
                    method: 'GET',
                    headers: new Headers({
                        'Authorization': 'Bearer ' + response.token
                    })
                });

                window.location.replace(response.url);
                /* fetch(redirectRequest).then(function (res) {
                     window.location.replace(response.url);
                 });*/
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // Xử lý lỗi
            console.log("Lỗi: " + errorThrown);
        }
    });
}