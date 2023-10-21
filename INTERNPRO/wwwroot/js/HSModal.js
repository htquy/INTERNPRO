
$(document).ready(function () {
   // Insert();
});
$('#showPost').on('click', function () {
    $('#postModal').modal('show');
});
function DetailM(ma) {
 
        $('#'+ma).modal('show');
}
function Insert(count) {
    var formData = new FormData();
    formData.append('maHs', $('#MaHs').val());
    formData.append('passWord', $('#PassWord').val());
    formData.append('hoTenHs', $('#HoTenHs').val());
    formData.append('queQuan', $('#QueQuan').val());
    formData.append('tenLop', $('#TenLop').val());
    formData.append('gioiTinh', $('#GioiTinh').val());
    formData.append('soDienThoaiHs', $('#SoDienThoaiHs').val());
    formData.append('ngaySinh', $('#NgaySinh').val());
    formData.append('hoTenPh', $('#HoTenPh').val());
    formData.append('soDienThoaiPh', $('#SoDienThoaiPh').val());
    var ImageFile = $('.input-image')[count].files[0];
    if (ImageFile) {
        formData.append('ImageFile', ImageFile);
    }
    console.log(formData);
    $.ajax({
        url: "/HocSinh/InsertHS",
        data: formData,
        type: 'post',
        processData: false, 
        contentType: false, 
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Nhập không đúng');
            } else {
                setTimeout(function () {
                    location.reload();
                    $('#postModal').modal('hide');
                }, 2000);
            }
        },
        error: function () {
            alert('Lỗi!!!');
        }
    });
}
function Delete(MaHS) {
    $.ajax({
        url: "/HocSinh/RemoveHS/" + MaHS,
        data: MaHS,
        type: "DELETE", 
        success: function (response) {
            setTimeout(function () {
                location.reload();
            }, 2000);
        },
        error: function () {
            alert('Lỗi khi xóa!!!');
        }
    });
}
function Update(count) {
    var formData = new FormData();
    formData.append('maHs', $($('.input-mahs')[count]).val());
    formData.append('passWord', $($('.input-pass')[count]).val());
    //var id =  MaHS + ".jpg";
    formData.append('hoTenHs', $($('.input-hths')[count]).val());
    var a = $($('.input-hths')[count]).val();
    formData.append('queQuan', $($('.input-quequan')[count]).val());
    formData.append('tenLop', $($('.input-lop')[count]).val());
    formData.append('gioiTinh', $($('.input-gioitinh')[count]).val());
    formData.append('soDienThoaiHs', $($('.input-sdths')[count]).val());
    formData.append('ngaySinh', $($('.input-ngaysinh')[count]).val());
    formData.append('hoTenPh', $($('.input-htph')[count]).val());
    formData.append('soDienThoaiPh', $($('.input-sdtph')[count]).val());
    var ImageFile = $('.input-image')[count].files[0];
    if (ImageFile) {
        formData.append('ImageFile', ImageFile);
    } else formData.append('ImageFile', null);
    console.log(formData);
    $.ajax({
        url: "/HocSinh/PutHS",
        data: formData,
        type: 'post',
        processData: false,
        contentType: false, 
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
function Logout() {
    $.ajax({
        url: "/HocSinh/Log",
        type: 'get',
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

