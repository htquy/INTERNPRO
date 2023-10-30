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
            } else { window.location.href = response.redirectUrl; }
        },
        error: function (xhr, textStatus, errorThrown) {
            // Xử lý lỗi
            console.log("Lỗi: " + errorThrown);
        }
    });
}