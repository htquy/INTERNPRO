function EditLuong(count) {
    var dsthuong = [];

    // Sử dụng jQuery để chọn tất cả các thẻ input có class "thuong"
    $(".thuong").each(function () {
        // Lấy giá trị của từng thẻ input và thêm vào mảng
        dsthuong.push($(this).val());
    });
    $.ajax({
        url: "/LuongGV/PostLuong",
        type: 'post',
        data: { dsthuong: dsthuong },
        success: function (response) {
            alert(response);
        },
        error: function (err) {
            alert(err);
        },
    });
}