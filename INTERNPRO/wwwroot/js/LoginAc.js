function Login(code, pass) {
    if (code == "0201866" && pass == "02122003") {
        window.location = "https:localhost:7275/HocSinh/GetHS";
    }
    else {
        $ajax({
            url: "Account/GetAccount",
            type: 'get',
            dataType: "text",
            data: {
                code=code,
                password=pass
            },
            success: function (response) {
                setTimeout(function () {
                    location.reload();
                }, 2000);
            },
            error: function () {
                alert('Lỗi khi update!!!');
            }
        });
    }
}