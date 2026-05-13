using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class MonAnRepository : IMonAnRepository
{
    private readonly AppDbContext _db;
    public MonAnRepository(AppDbContext db) => _db = db;

    public Task<List<MonAn>> GetAllAsync() =>
        _db.MonAns.AsNoTracking().OrderBy(x => x.MaMon).ToListAsync();

    public Task<MonAn?> GetByIdAsync(string maMon) =>
        _db.MonAns.AsNoTracking().FirstOrDefaultAsync(x => x.MaMon == maMon);

    public async Task<bool> AddAsync(MonAn m)
    {
        _db.MonAns.Add(m);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(MonAn m)
    {
        _db.MonAns.Update(m);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(string maMon)
    {
        var entity = await _db.MonAns.FindAsync(maMon);
        if (entity == null) return false;
        _db.MonAns.Remove(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<string> GetNewIdAsync()
    {
        var last = await _db.MonAns
            .AsNoTracking()
            .OrderByDescending(x => x.MaMon)
            .Select(x => x.MaMon)
            .FirstOrDefaultAsync();

        if (last != null && last.Length >= 2 && int.TryParse(last[1..], out int num))
            return $"M{(num + 1):D2}";
        return "M01";
    }

    public Task<bool> IsExistsTenMonAsync(string tenMon) =>
        _db.MonAns.AnyAsync(x => x.TenMon.ToLower() == tenMon.ToLower());

    public Task<int> CountAllAsync() => _db.MonAns.CountAsync();
}
