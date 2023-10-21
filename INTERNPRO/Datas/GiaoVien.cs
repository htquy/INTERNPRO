using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class GiaoVien
{
    [Required]
    public int MaGv { get; set; }
    [Required]
    public string PassWord { get; set; }
    [Required]
    public string TenGv { get; set; } 
    [Required]
    public string GioiTinh { get; set; } 
    [Required]
    public DateTime NgaySinh { get; set; }
    [Required]
    public string QueQuan { get; set; } 
    [Required]
    public string ChuyenMon { get; set; }

    public string? Anh { get; set; }
    [Required]
    public DateTime NgayBatDau { get; set; }

    public string? Motakhac { get; set; }
    [Required]
    public int MaLuong { get; set; }

    public string? ChuNhiemLop { get; set; }

    [Required]
    public virtual Luong MaLuongNavigation { get; set; } 

    public virtual ICollection<PhanCongCt> PhanCongCts { get; set; } = new List<PhanCongCt>();
}
