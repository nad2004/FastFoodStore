using Domain.Entities;

namespace Application.Interfaces;

public interface IHoaDonRepository
{
    Task<List<HoaDon>> GetAllAsync();
    Task<HoaDon?> GetByIdAsync(string maHD);
    Task<List<ChiTietHoaDon>> GetChiTietAsync(string maHD);
    Task<bool> AddAsync(HoaDon hd);
    Task<bool> AddChiTietAsync(string maHD, List<ChiTietHoaDon> chiTiet);
    Task<bool> UpdateAsync(HoaDon hd);
    Task<bool> DeleteAsync(string maHD);
    Task<string> GetNewIdAsync();
    Task<decimal> SumAllTongTienAsync();
    Task<int> CountAllAsync();
    Task<List<ThongKeDoanhThu>> GetDoanhThuTheoNgayAsync(string dateFrom, string dateTo);
}
