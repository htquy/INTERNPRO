using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace INTERNPRO.Models;

public partial class HocSinhs
{
    public int MaHs { get; set; }
    [Required]
    public string PassWord { get; set; }
    [Required]
    public string HoTenHs { get; set; }
    [Required]
    public string QueQuan { get; set; }
    [Required]
    public string TenLop { get; set; }
    [Required]
    public string GioiTinh { get; set; }

    public int SoDienThoaiHs { get; set; }

    public DateTime NgaySinh { get; set; }
    [Required]
    public string HoTenPh { get; set; }

    public int? SoDienThoaiPh { get; set; }

    public string? Anh { get; set; }

    //public virtual LopHoc TenLopNavigation { get; set; } = null!;
}
