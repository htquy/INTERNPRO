function ShowPCCT(TenL) {
    window.location = "https://localhost:7275/PCCT/GetCalendars/" + TenL;
}
function PhanCong(lop,ma) {
    $.ajax({
        url: "/PCCT/GetCalendars/" + lop +"/"+ ma,
        type: 'get',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Hỏng');
            } else {
                var object = ''
                $.each(response, function (index, item) {
                    object += '<option value="'+item.maGv+'-'+item.chuyenMon+'"></option>'
                });
                $('#dsgvmhs').html(object);
                $('#listGVMH').modal('show');
            }
        },
        error: function () {
            alert('Lỗi!!!');
        }
    })
}