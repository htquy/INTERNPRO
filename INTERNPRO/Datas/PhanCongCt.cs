using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class PhanCongCt
{
    public int MaCt { get; set; }

    public int MaGv { get; set; }

    public int MaMh { get; set; }

    public string TenLop { get; set; } = null!;

    public int Ca { get; set; }

    public DateTime Ngay { get; set; }

    public virtual GiaoVien MaGvNavigation { get; set; } = null!;

    public virtual MonHoc MaMhNavigation { get; set; } = null!;

    public virtual LopHoc TenLopNavigation { get; set; } = null!;
}
