function Log_out() {
    $.ajax({
        type: "POST",
        url: "/Account/Logout",
        data: {},
        success: function () {
            // Xóa cookie trên phía client
            document.cookie = "YourAppCookieName=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";

            // Redirect hoặc thực hiện các bước khác sau logout
            window.location.href = "/Home/Index";
        },
        error: function () {
            // Xử lý lỗi nếu cần
        }
    });
}