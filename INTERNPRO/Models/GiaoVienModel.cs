using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTERNPRO.Models;

public partial class GiaoVienModel
{
    public int MaGv { get; set; }
    [Required]
    public string PassWord { get; set; }
    [Required]
    public string HoTenGv { get; set; }
    [Required]
    public string QueQuan { get; set; }
    [Required]
    public string ChuyenMon { get; set; }
    [Required]
    public string GioiTinh { get; set; }

    public int MaLuong { get; set; }

    public DateTime NgaySinh { get; set; }
    [Required]
    public DateTime NgayBatDau { get; set; }

    public string? MoTaKhac { get; set; }

    public string? ChuNhiemLop { get; set; }
    public string SoDienThoaiGV { get; set; }

    public string? Anh { get; set; }
    [NotMapped]
    [DisplayName("Upload File")]
    public IFormFile? ImageFile { get; set; }

    //public virtual LopHoc TenLopNavigation { get; set; } = null!;
}