function CloseModal() {
    $('#listGVMH').modal('hide');
}
function ShowPCCT(TenL) {
    window.location = "https://localhost:7275/PCCT/GetCalendars/" + TenL;
}
function PhanCong(lop, ca) {
    $.ajax({
        url: "/PCCT/GetCalendars/" + lop + "/" + ca,
        type: 'get',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Hỏng');
            } else {
                var object = '';
                var p = '';
                response = JSON.parse(response);
                $.each(response, function (index, item) {
                    object += '<option value="' + item.MaGv + '-' + item.ChuyenMon + '"></option>'
                });
                $('#listGVMH').modal('show');
                p += '<button type="button" class="btn btn-primary" onclick="PostPCCT(\'' + lop.toString() + '\', \'' + ca.toString() + '\')">Select</button>';
                p += '<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>';
                $('#dsgvmhs').html(object);
                $('#postcalen').html(p);
            }
        },
        error: function (xhr, status, error) {
            var errorMessage = 'Lỗi: ' + error;
            alert(errorMessage);
        }
    });
}
function PostPCCT(lop, ca) {
    str = document.getElementById('dsgvmh').value,

        $.ajax({
            url: "/PCCT/GetCalendars/" + lop + "/" + ca + "/" + str,
            type: 'post',
            data: str,
            success: function (response) {
                if (response == null || response == undefined || response.length == 0) {
                    alert('Hỏng');
                } else {
                    alert(response);
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // Hiển thị lỗi từ phản hồi
                console.log('Lỗi: ' + jqXHR.status + ' - ' + jqXHR.statusText);
                console.log('Lỗi chi tiết: ' + jqXHR.responseText);
            }
        });
}
function DeletePC(ma) {
    $.ajax({
        url: "/PCCT/GetCalendars/" + ma,
        type: 'delete',
        data: ma,
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Hỏng');
            } else {
                alert(response);
                setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Hiển thị lỗi từ phản hồi
            console.log('Lỗi: ' + jqXHR.status + ' - ' + jqXHR.statusText);
            console.log('Lỗi chi tiết: ' + jqXHR.responseText);
        }
    });
}