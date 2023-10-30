namespace INTERNPRO.Models
{
    public class DSDiemHSModel
    {
        public int MaMh { get; set; }
        public int? MaHs { get; set; }
        public double? Diem15 { get; set; }

        public double? DiemGk { get; set; }

        public double? DiemCk { get; set; }

        public int Ki { get; set; }

        public string Lop { get; set; } = null!;

        public string TenMh { get; set; }
    }
}
