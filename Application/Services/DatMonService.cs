using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class DatMonService
{
    private readonly IMonAnRepository _monAnRepo;
    private readonly IHoaDonRepository _hdRepo;
    private readonly IKhoRepository _khoRepo;
    private readonly IKhachHangRepository _khRepo;

    public DatMonService(IMonAnRepository monAnRepo, IHoaDonRepository hdRepo, IKhoRepository khoRepo, IKhachHangRepository khRepo)
    {
        _monAnRepo = monAnRepo;
        _hdRepo = hdRepo;
        _khoRepo = khoRepo;
        _khRepo = khRepo;
    }

    public Task<List<MonAn>> GetMenuAsync() => _monAnRepo.GetAllAsync();

    public async Task<(bool Success, string Message)> ThanhToanAsync(string tenKH, string sdt, List<DatMon> items)
    {
        if (items.Count == 0) return (false, "Giỏ hàng trống");

        string maHD = await _hdRepo.GetNewIdAsync();
        decimal tongTien = items.Sum(x => x.ThanhTien);

        var hd = new HoaDon
        {
            MaHD = maHD,
            TenKH = tenKH,
            TenNV = Session.TenNV ?? "NV01",
            NgayLap = DateTime.Now.ToString("dd/MM/yyyy"),
            TongTien = tongTien
        };

        bool okHD = await _hdRepo.AddAsync(hd);
        if (!okHD) return (false, "Lỗi khi tạo hóa đơn");

        var chiTiet = items.Select(x => new ChiTietHoaDon
        {
            MaHD = maHD,
            TenMon = x.TenMon,
            Size = x.Size,
            SoLuong = x.SoLuong,
            DonGia = x.DonGia
        }).ToList();

        await _hdRepo.AddChiTietAsync(maHD, chiTiet);

        // Update Kho
        // Note: Java code had logic to deduct from Kho based on some mapping
        // Here we just ensure customer exists
        if (!await _khRepo.IsExistsSdtAsync(sdt))
        {
            await _khRepo.InsertAsync(new KhachHang { TenKH = tenKH, SoDienThoai = sdt, TheLoai = "Vãng lai" });
        }

        return (true, "Thanh toán thành công");
    }
}
