namespace Domain.Entities;

public class DanhMuc
{
    public string MaDanhMuc { get; set; } = null!;
    public string TenDanhMuc { get; set; } = null!;

    public DanhMuc() { }
    public DanhMuc(string ma, string ten)
    {
        MaDanhMuc = ma;
        TenDanhMuc = ten;
    }
}
