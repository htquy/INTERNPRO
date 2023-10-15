using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class Luong
{
    public int MaLuong { get; set; }

    public int? Thuong { get; set; }

    public int MucLuongCb { get; set; }

    public virtual ICollection<GiaoVien> GiaoViens { get; set; } = new List<GiaoVien>();
}
