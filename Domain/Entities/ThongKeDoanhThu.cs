namespace Domain.Entities;

public class ThongKeDoanhThu
{
    public string ThoiGian { get; set; } = null!;
    public decimal TongDoanhThu { get; set; }

    public ThongKeDoanhThu() { }
    public ThongKeDoanhThu(string thoiGian, decimal tong)
    {
        ThoiGian = thoiGian;
        TongDoanhThu = tong;
    }
}
