using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class MonHoc
{
    public int MaMh { get; set; }

    public string TenMh { get; set; } = null!;

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual ICollection<PhanCongCt> PhanCongCts { get; set; } = new List<PhanCongCt>();
}
