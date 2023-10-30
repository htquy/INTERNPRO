using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class GiaoVien
{
    public int MaGv { get; set; }

    public string PassWord { get; set; } = null!;

    public string TenGv { get; set; } = null!;

    public string GioiTinh { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string QueQuan { get; set; } = null!;

    public string ChuyenMon { get; set; } = null!;

    public string? Anh { get; set; }

    public DateTime NgayBatDau { get; set; }

    public string? Motakhac { get; set; }

    public int MaLuong { get; set; }

    public string? ChuNhiemLop { get; set; }

    public int? ThuongThem { get; set; }

    public string? SoDienThoaiGv { get; set; }

    public virtual Luong MaLuongNavigation { get; set; } = null!;

    public virtual ICollection<PhanCongCt> PhanCongCts { get; set; } = new List<PhanCongCt>();
}
