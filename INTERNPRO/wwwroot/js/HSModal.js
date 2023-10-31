function CloseModal() {
    $('#detail').modal('hide');
    $('#postModal').modal('hide');
}

$('#showPost').on('click', function () {
   $('#postModal').modal('show');
});
function DetailM(ma) {
    $.ajax({
        url: "/HocSinh/GetDetailHS/" + ma,
        data: ma,
        type: 'get',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Hỏng');
            } else {
                var object = '';
                $('#detail #MaHs').val(response.maHs);
                $('#detail #PassWord').val(response.passWord);
                $('#detail #HoTenHs').val(response.hoTenHs);
                $('#detail #QueQuan').val(response.queQuan);
                $('#detail #TenLop').val(response.tenLop);
                $('#detail #GioiTinh').val(response.gioiTinh);
                $('#detail #SoDienThoaiHs').val(response.soDienThoaiHs);
                $('#detail #NgaySinh').val(response.ngaySinh);
                $('#detail #SoDienThoaiPh').val(response.soDienThoaiPh);
                $('#detail #HoTenPh').val(response.hoTenPh);
                $('#detail #NgayNh').val(response.ngayNh);
                var imagePath = '/Image/' + response.anh;
                object += '<label asp-for="ImageFile" class="col-sm-2 col-form-label"></label>' +
                    '<img src="' + imagePath + '" asp-append-version="true" width="150px">';
                $('#anh_HS').html(object);
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
    formData.append('maHs', $('#postModal #MaHs').val());
    formData.append('passWord', $('#postModal #PassWord').val());
    formData.append('hoTenHs', $('#postModal #HoTenHs').val());
    formData.append('queQuan', $('#postModal #QueQuan').val());
    formData.append('tenLop', $('#postModal #TenLop').val());
    formData.append('gioiTinh', $('#postModal #GioiTinh').val());
    formData.append('soDienThoaiHs', $('#postModal #SoDienThoaiHs').val());
    formData.append('ngaySinh', $('#postModal #NgaySinh').val());
    formData.append('hoTenPh', $('#postModal #HoTenPh').val());
    formData.append('soDienThoaiPh', $('#postModal #SoDienThoaiPh').val());
    formData.append('ngayNh', $('#postModal #NgayNh').val());
    var ImageFile = $('#postModal #ImageFile')[0].files[0];
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
    formData.append('maHs', $('#detail #MaHs').val());
    formData.append('passWord', $('#detail #PassWord').val());
    formData.append('hoTenHs', $('#detail #HoTenHs').val());
    formData.append('queQuan', $('#detail #QueQuan').val());
    formData.append('tenLop', $('#detail #TenLop').val());
    formData.append('gioiTinh', $('#detail #GioiTinh').val());
    formData.append('soDienThoaiHs', $('#detail #SoDienThoaiHs').val());
    formData.append('ngaySinh', $('#detail #NgaySinh').val());
    formData.append('hoTenPh', $('#detail #HoTenPh').val());
    formData.append('soDienThoaiPh', $('#detail #SoDienThoaiPh').val());
    formData.append('ngayNh', $('#detail #NgayNh').val());
    var ImageFile = $('#detail #ImageFile')[0].files[0];
    if (ImageFile) {
        formData.append('ImageFile', ImageFile);
    }
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
            window.location.href = response.redirectUrl; 
        },
        error: function (xhr, textStatus, errorThrown) {
            // Xử lý lỗi
            console.log("Lỗi: " + errorThrown);
        }
    });
    
}

