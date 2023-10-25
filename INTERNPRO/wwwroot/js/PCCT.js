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
                $.each(response, function (index, item) {
                    object += '<option value="' + item.maGv + '-' + item.chuyenMon + '"></option>'
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
        url: "/PCCT/GetCalendars/" + lop + "/" + ca+"/"+str,
        type: 'post',
        data:str,
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Hỏng');
            } else { }
        },
        error: function () {
            alert('Lỗi ít thôi');
        }
    });
}
function DeletePC(ma) {
    $.ajax({
        url: "/PCCT/GetCalendars/" +ma,
        type: 'delete',
        data: ma,
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Hỏng');
            } else { }
        },
        error: function () {
            alert('Lỗi ít thôi');
        }
        })
}