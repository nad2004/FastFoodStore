using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class KhoRepository : IKhoRepository
{
    private readonly AppDbContext _db;
    public KhoRepository(AppDbContext db) => _db = db;

    public Task<List<Kho>> GetAllAsync() =>
        _db.Khos.AsNoTracking().OrderBy(x => x.MaHH).ToListAsync();

    public Task<Kho?> GetByIdAsync(string maHH) =>
        _db.Khos.AsNoTracking().FirstOrDefaultAsync(x => x.MaHH == maHH);

    public async Task<bool> AddAsync(Kho item)
    {
        _db.Khos.Add(item);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Kho item)
    {
        _db.Khos.Update(item);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(string maHH)
    {
        var entity = await _db.Khos.FindAsync(maHH);
        if (entity == null) return false;
        _db.Khos.Remove(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    public Task<List<string>> GetAllMaHHAsync() =>
        _db.Khos.AsNoTracking().OrderBy(x => x.MaHH).Select(x => x.MaHH).ToListAsync();

    public async Task<int> GetSoLuongTonAsync(string maHH)
    {
        var item = await _db.Khos.AsNoTracking().FirstOrDefaultAsync(x => x.MaHH == maHH);
        return item?.SoLuong ?? 0;
    }

    public async Task TruKhoAsync(string maHH, int soLuongMua)
    {
        var item = await _db.Khos.FindAsync(maHH);
        if (item != null)
        {
            item.SoLuong -= soLuongMua;
            await _db.SaveChangesAsync();
        }
    }
}
