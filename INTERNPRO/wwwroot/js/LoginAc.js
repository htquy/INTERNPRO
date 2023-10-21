function Login(id, pass) {
        var coder = id.toString();
        $.ajax({
            url: "Account/GetAccount",
            type: 'post',
            data:{
                code:coder,
                password:pass
            },
            success: function (response) {
                // Xử lý thành công, ví dụ: chuyển hướng hoặc hiển thị thông báo
                window.location.href = response.redirectUrl; // Đây bạn có thể lấy địa chỉ chuyển hướng từ response nếu cần.
            },
            error: function (xhr, textStatus, errorThrown) {
                // Xử lý lỗi
                console.log("Lỗi: " + errorThrown);
            } 
        });
    }