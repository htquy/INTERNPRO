function CloseModal() {
    $('#detail').modal('hide');
    $('#postModal').modal('hide');
}
$(document).ready(function () {
    // Insert();
});
$('#showPost').on('click', function () {
    $('#postModal').modal('show');
});
function DetailM(ma) {
    $.ajax({
        url: "/GiaoVien/GetGV/" + ma,
        data: ma,
        type: 'get',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Hỏng');
            } else {
                var object = '';
                $('#detail #MaGv').val(response.maGv);
                $('#detail #PassWord').val(response.passWord);
                $('#detail #HoTenGv').val(response.tenGv);
                $('#detail #QueQuan').val(response.queQuan);
                $('#detail #ChuyenMon').val(response.chuyenMon);
                $('#detail #GioiTinh').val(response.gioiTinh);
                $('#detail #MaLuong').val(response.maLuong);
                $('#detail #NgaySinh').val(response.ngaySinh);
                $('#detail #SoDienThoaiGV').val(response.soDienThoaiGv);
                $('#detail #NgayBatDau').val(response.ngayBatDau);
                $('#detail #MoTaKhac').val(response.moTaKhac);
                $('#detail #ChuNhiemLop').val(response.chuNhiemLop);
                var imagePath = '../Image/' + response.anh;
                object += '<label asp-for="ImageFile" class="col-sm-2 col-form-label"></label>' +
                    '<img src="' + imagePath + '" asp-append-version="true" width="150px">';

                $('#anh_GV').html(object);
                $('#detail').modal('show'); 
            }
        },
        error: function () {
            alert('Lỗi!!!');
        }
    })
}


function Insert(count) {
    var formData = new FormData();
    formData.append('maGv', $('#postModal #MaGv').val());
    formData.append('passWord', $('#postModal #PassWord').val());
    formData.append('hoTenGv', $('#postModal #HoTenGv').val());
    formData.append('queQuan', $('#postModal #QueQuan').val());
    formData.append('chuyenMon', $('#postModal #ChuyenMon').val());
    formData.append('gioiTinh', $('#postModal #GioiTinh').val());
    formData.append('maLuong', $('#postModal #MaLuong').val());
    formData.append('ngaySinh', $('#postModal #NgaySinh').val());
    formData.append('soDienThoaiGv', $('#postModal #SoDienThoaiGV').val());
    formData.append('ngayBatDau', $('#postModal #NgayBatDau').val());
    formData.append('moTaKhac', $('#postModal #MoTaKhac').val());
    formData.append('chuNhiemLop', $('#postModal #ChuNhiemLop').val());
    var ImageFile = $('#postModal #ImageFile')[0].files[0];
    if (ImageFile) {
        formData.append('ImageFile', ImageFile);
    }
    console.log(formData);
    $.ajax({
        url: "/GiaoVien/InsertGV",
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
function Delete(MaGV) {
    $.ajax({
        url: "/GiaoVien/RemoveGV/" + MaGV,
        data: MaGV,
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
    formData.append('maGv', $('#detail #MaGv').val());
    formData.append('passWord', $('#detail #PassWord').val());
    formData.append('hoTenGv', $('#detail #HoTenGv').val());
    formData.append('queQuan', $('#detail #QueQuan').val());
    formData.append('chuyenMon', $('#detail #ChuyenMon').val());
    formData.append('gioiTinh', $('#detail #GioiTinh').val());
    formData.append('maLuong', $('#detail #MaLuong').val());
    formData.append('ngaySinh', $('#detail #NgaySinh').val());
    formData.append('soDienThoaiGv', $('#detail #SoDienThoaiGV').val());
    formData.append('ngayBatDau', $('#detail #NgayBatDau').val());
    formData.append('moTaKhac', $('#detail #MoTaKhac').val());
    formData.append('chuNhiemLop', $('#detail #ChuNhiemLop').val());
    var ImageFile = $('#detail #ImageFile')[0].files[0];
    if (ImageFile) {
        formData.append('ImageFile', ImageFile);
    }
    console.log(formData);
    $.ajax({
        url: "/GiaoVien/PutGV",
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

