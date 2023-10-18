
$(document).ready(function () {
   // Insert();
});
$('#showPost').on('click', function () {
    $('#postModal').modal('show');
});
function DetailM(mahs) {
 
        $('#'+mahs).modal('show');
}
function Insert() {
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
    var ImageFile = $('#ImageFile')[0].files[0];

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
function Update(ma) {
    var formData = new FormData();
    formData.append('maHs', document.getElementById('mahs').value);
    formData.append('passWord', document.getElementById('password').value);
    formData.append('hoTenHs', document.getElementById('hotenhs').value);
    formData.append('queQuan', document.getElementById('quequan').value);
    formData.append('tenLop', document.getElementById('lop').value);
    formData.append('gioiTinh', document.getElementById('gioitinh').value);
    formData.append('soDienThoaiHs', document.getElementById('sdths').value);
    formData.append('ngaySinh', document.getElementById('ngaysinh').value);
    formData.append('hoTenPh', document.getElementById('hotenph').value);
    formData.append('soDienThoaiPh', document.getElementById('sdtph').value);
    $.ajax({
        url: "/HocSinh/PutHS/" + ma,
        data: formData,
        type: "PUT",
        processData: false,
        contentType: false, 
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

