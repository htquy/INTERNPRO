using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class LopHoc
{
    public string TenLop { get; set; } = null!;

    public int Gvcn { get; set; }

    public int SoLuongChoNgoi { get; set; }

    public virtual ICollection<HocSinh> HocSinhs { get; set; } = new List<HocSinh>();

    public virtual ICollection<PhanCongCt> PhanCongCts { get; set; } = new List<PhanCongCt>();
}
