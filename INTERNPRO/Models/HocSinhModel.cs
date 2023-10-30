using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTERNPRO.Models;

public partial class HocSinhModel
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

    public string SoDienThoaiHs { get; set; }

    public DateTime NgaySinh { get; set; }
    [Required]
    public string HoTenPh { get; set; }

    public string? SoDienThoaiPh { get; set; }

    public DateTime? NgayNh { get; set; }
    public string? Anh { get; set; }
    [NotMapped]
    [DisplayName("Upload File")]
    public IFormFile? ImageFile { get; set; }

    //public virtual LopHoc TenLopNavigation { get; set; } = null!;
}