using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class LopHoc
{
    public string TenLop { get; set; } = null!;

    public string Gvcn { get; set; } = null!;

    public int SoLuongChoNgoi { get; set; }

    public virtual ICollection<PhanCongCt> PhanCongCts { get; set; } = new List<PhanCongCt>();
}
