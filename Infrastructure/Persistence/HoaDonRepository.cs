using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class HoaDonRepository : IHoaDonRepository
{
    private readonly AppDbContext _db;
    public HoaDonRepository(AppDbContext db) => _db = db;

    public Task<List<HoaDon>> GetAllAsync() =>
        _db.HoaDons.AsNoTracking().OrderByDescending(x => x.MaHD).ToListAsync();

    public Task<HoaDon?> GetByIdAsync(string maHD) =>
        _db.HoaDons.AsNoTracking().FirstOrDefaultAsync(x => x.MaHD == maHD);

    public Task<List<ChiTietHoaDon>> GetChiTietAsync(string maHD) =>
        _db.ChiTietHoaDons.AsNoTracking().Where(x => x.MaHD == maHD).ToListAsync();

    public async Task<bool> AddAsync(HoaDon hd)
    {
        _db.HoaDons.Add(hd);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddChiTietAsync(string maHD, List<ChiTietHoaDon> chiTiet)
    {
        foreach (var ct in chiTiet) ct.MaHD = maHD;
        _db.ChiTietHoaDons.AddRange(chiTiet);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(HoaDon hd)
    {
        _db.HoaDons.Update(hd);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(string maHD)
    {
        var entity = await _db.HoaDons.FindAsync(maHD);
        if (entity == null) return false;
        _db.HoaDons.Remove(entity); // cascade xóa ChiTiet
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<string> GetNewIdAsync()
    {
        var last = await _db.HoaDons
            .AsNoTracking()
            .OrderByDescending(x => x.MaHD)
            .Select(x => x.MaHD)
            .FirstOrDefaultAsync();

        if (last != null && last.Length >= 4 && int.TryParse(last[2..], out int num))
            return $"HD{(num + 1):D3}";
        return "HD001";
    }

    public async Task<decimal> SumAllTongTienAsync() =>
        await _db.HoaDons.SumAsync(x => x.TongTien);

    public Task<int> CountAllAsync() => _db.HoaDons.CountAsync();

    public async Task<List<ThongKeDoanhThu>> GetDoanhThuTheoNgayAsync(string dateFrom, string dateTo)
    {
        // Parse ngày dạng DD/MM/YYYY
        if (!DateTime.TryParseExact(dateFrom, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var fromDate))
            return [];

        if (!DateTime.TryParseExact(dateTo, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var toDate))
            return [];

        // Lọc và group theo NgayLap (string DD/MM/YYYY — query phía client)
        var all = await _db.HoaDons.AsNoTracking().ToListAsync();

        return all
            .Where(h =>
            {
                if (!DateTime.TryParseExact(h.NgayLap, "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out var d)) return false;
                return d >= fromDate && d <= toDate;
            })
            .GroupBy(h => h.NgayLap)
            .OrderBy(g => DateTime.ParseExact(g.Key, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture))
            .Select(g => new ThongKeDoanhThu(g.Key, g.Sum(h => h.TongTien)))
            .ToList();
    }
}
