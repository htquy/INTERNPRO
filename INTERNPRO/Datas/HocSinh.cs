using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class HocSinh
{
    public int MaHs { get; set; }

    public string PassWord { get; set; } = null!;

    public string HoTenHs { get; set; } = null!;

    public string QueQuan { get; set; } = null!;

    public string TenLop { get; set; } = null!;

    public string GioiTinh { get; set; } = null!;

    public string SoDienThoaiHs { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string HoTenPh { get; set; } = null!;

    public string? SoDienThoaiPh { get; set; }

    public string? Anh { get; set; }

    public DateTime? NgayNh { get; set; }

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();
}
