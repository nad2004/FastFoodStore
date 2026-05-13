using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DanhMucRepository : IDanhMucRepository
{
    private readonly AppDbContext _db;
    public DanhMucRepository(AppDbContext db) => _db = db;

    public Task<List<DanhMuc>> GetAllAsync() =>
        _db.DanhMucs.AsNoTracking().OrderBy(x => x.MaDanhMuc).ToListAsync();

    public async Task<bool> InsertAsync(string ma, string ten)
    {
        _db.DanhMucs.Add(new DanhMuc(ma, ten));
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(string ma, string ten)
    {
        var entity = await _db.DanhMucs.FindAsync(ma);
        if (entity == null) return false;
        entity.TenDanhMuc = ten;
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(string ma)
    {
        var entity = await _db.DanhMucs.FindAsync(ma);
        if (entity == null) return false;
        _db.DanhMucs.Remove(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    public Task<List<string>> GetAllTenDanhMucAsync() =>
        _db.DanhMucs.AsNoTracking().Select(x => x.TenDanhMuc).ToListAsync();

    public Task<bool> IsExistsTenDanhMucAsync(string ten) =>
        _db.DanhMucs.AnyAsync(x => x.TenDanhMuc.ToLower() == ten.ToLower());
}
