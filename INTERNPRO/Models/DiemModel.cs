namespace INTERNPRO.Models
{
    public class DiemModel
    {
        public int MaHs { get; set; }
        public string HoTenHs { get; set; }
        public int Ki { get; set; }
        public string Lop { get; set; } = null!;
        public double? Diem15 { get; set; }

        public double? DiemGk { get; set; }

        public double? DiemCk { get; set; }
        public int MaMh { get; set; }
    }
}
