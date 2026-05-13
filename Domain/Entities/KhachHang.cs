namespace Domain.Entities;

public class KhachHang
{
    public string MaKH { get; set; } = null!;
    public string TenKH { get; set; } = null!;
    public string? TheLoai { get; set; }
    public string? GioiTinh { get; set; }
    public string? Email { get; set; }
    public string? SoDienThoai { get; set; }
    public string? DiaChi { get; set; }
}
