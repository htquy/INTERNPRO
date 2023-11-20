using System;
using System.Collections.Generic;

namespace INTERNPRO.Datas;

public partial class Hphi
{
    public string Ki { get; set; } = null!;

    public int MaHs { get; set; }

    public int Hpki { get; set; }

    public int NoHpki { get; set; }

    public int Hpthang { get; set; }

    public int NoHpthang { get; set; }

    public virtual HocSinh MaHsNavigation { get; set; } = null!;
}
