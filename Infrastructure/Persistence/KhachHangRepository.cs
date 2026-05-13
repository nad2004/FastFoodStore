using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class KhachHangRepository : IKhachHangRepository
{
    private readonly AppDbContext _db;
    public KhachHangRepository(AppDbContext db) => _db = db;

    public Task<List<KhachHang>> GetAllAsync() =>
        _db.KhachHangs.AsNoTracking().OrderBy(x => x.MaKH).ToListAsync();

    public Task<KhachHang?> GetByIdAsync(string maKH) =>
        _db.KhachHangs.AsNoTracking().FirstOrDefaultAsync(x => x.MaKH == maKH);

    public async Task<bool> InsertAsync(KhachHang kh)
    {
        _db.KhachHangs.Add(kh);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(KhachHang kh)
    {
        _db.KhachHangs.Update(kh);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(string maKH)
    {
        var entity = await _db.KhachHangs.FindAsync(maKH);
        if (entity == null) return false;
        _db.KhachHangs.Remove(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<string> GetNewIdAsync()
    {
        var last = await _db.KhachHangs
            .AsNoTracking()
            .OrderByDescending(x => x.MaKH)
            .Select(x => x.MaKH)
            .FirstOrDefaultAsync();

        if (last != null && last.StartsWith("KH") && int.TryParse(last[2..], out int num))
            return $"KH{(num + 1):D3}";
        return "KH001";
    }

    public Task<bool> IsExistsSdtAsync(string sdt) =>
        _db.KhachHangs.AnyAsync(x => x.SoDienThoai == sdt);

    public Task<int> CountAllAsync() => _db.KhachHangs.CountAsync();
}
