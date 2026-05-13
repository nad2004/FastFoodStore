namespace Domain.Entities;

public class HoaDon
{
    public string MaHD { get; set; } = null!;
    public string? TenNV { get; set; }
    public string? TenKH { get; set; }
    public string? NgayLap { get; set; }
    public decimal TongTien { get; set; }
}
