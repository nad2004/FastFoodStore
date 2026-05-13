namespace Domain.Entities;

public class DatMon
{
    public string MaMon { get; set; } = null!;
    public string TenMon { get; set; } = null!;
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public string Size { get; set; } = "M";
    public decimal ThanhTien => SoLuong * DonGia;

    public DatMon() { }
    public DatMon(string ma, string ten, int sl, decimal gia, string size = "M")
    {
        MaMon = ma;
        TenMon = ten;
        SoLuong = sl;
        DonGia = gia;
        Size = size;
    }
}
