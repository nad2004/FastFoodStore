namespace Domain.Entities;

public class MonAn
{
    public string MaMon { get; set; } = null!;
    public string TenMon { get; set; } = null!;
    public decimal DonGia { get; set; }
    public string? DonViTinh { get; set; }
    public string? HinhAnh { get; set; }
    public string? TenDanhMuc { get; set; }
    public string? MaDanhMuc { get; set; }
}
