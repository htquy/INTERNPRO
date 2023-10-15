using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class Diem
{
    public int MaHs { get; set; }

    public int MaMh { get; set; }

    public double? Diem15 { get; set; }

    public double? DiemGk { get; set; }

    public double? DiemCk { get; set; }

    public virtual HocSinh MaHsNavigation { get; set; } = null!;

    public virtual MonHoc MaMhNavigation { get; set; } = null!;
}
