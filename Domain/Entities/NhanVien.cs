namespace Domain.Entities;

public class NhanVien
{
    public string MaNV { get; set; } = null!;
    public string TenNV { get; set; } = null!;
    public string? NgaySinh { get; set; }
    public string? GioiTinh { get; set; }
    public string? ChucVu { get; set; }
    public string? SoDienThoai { get; set; }
    public string? DiaChi { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}
