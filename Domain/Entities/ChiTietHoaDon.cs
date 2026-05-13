namespace Domain.Entities;

public class ChiTietHoaDon
{
    public string MaHD { get; set; } = null!;
    public string TenMon { get; set; } = null!;
    public string? Size { get; set; }
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien => SoLuong * DonGia;
}
