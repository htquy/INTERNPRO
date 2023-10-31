function postDiem(lop,ma) {
    var Diem15=[];
    var MaHs = [];
    var HoTenHs = [];
    var Ki = [];
    var DiemGk=[];
    var DiemCk=[];
    var listdiem = []       ;
    $(".MaHs").each(function () {
        MaHs.push($(this).val());
    });
    $(".HoTenHs").each(function () {
        HoTenHs.push($(this).val());
    });
    $(".Ki").each(function () {
        Ki.push($(this).val());
    });
    $(".Diem15").each(function () {
        Diem15.push($(this).val());
    });
    $(".DiemGk").each(function () {
        DiemGk.push($(this).val());
    });
    $(".DiemCk").each(function () {
        DiemCk.push($(this).val());
    });
    for (let i = 0; i < Diem15.length; i++) {
        listdiem.push({
            MaHs: MaHs[i],
            HoTenHs: HoTenHs[i],
            Ki: Ki[i],
            Lop: lop,
            Diem15: Diem15[i],
            DiemGk: DiemGk[i],
            DiemCk: DiemCk[i],
            MaMh: ma
        });
    }
    $.ajax({
        url: "/Diem/PostDiem/"+lop+"/"+ma,
        type: "post",
        data: {
            listdiem: listdiem
        },
        success: function (response) {
            alert(response);
        },
        error: function (err) {
            alert(err);
        },
    });
}