using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class NhanVienRepository : INhanVienRepository
{
    private readonly AppDbContext _db;
    public NhanVienRepository(AppDbContext db) => _db = db;

    public Task<List<NhanVien>> GetAllAsync() =>
        _db.NhanViens.AsNoTracking().OrderBy(x => x.MaNV).ToListAsync();

    public Task<NhanVien?> GetByIdAsync(string maNV) =>
        _db.NhanViens.AsNoTracking().FirstOrDefaultAsync(x => x.MaNV == maNV);

    public async Task<bool> InsertAsync(NhanVien nv)
    {
        _db.NhanViens.Add(nv);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(NhanVien nv)
    {
        _db.NhanViens.Update(nv);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(string maNV)
    {
        var entity = await _db.NhanViens.FindAsync(maNV);
        if (entity == null) return false;
        _db.NhanViens.Remove(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    public Task<List<NhanVien>> SearchAsync(string keyword) =>
        _db.NhanViens.AsNoTracking()
            .Where(x => x.TenNV.ToLower().Contains(keyword.ToLower())
                     || x.MaNV.ToLower().Contains(keyword.ToLower())
                     || x.SoDienThoai.Contains(keyword))
            .ToListAsync();

    public async Task<string> GetNewIdAsync()
    {
        var last = await _db.NhanViens
            .AsNoTracking()
            .OrderByDescending(x => x.MaNV)
            .Select(x => x.MaNV)
            .FirstOrDefaultAsync();

        if (last != null && last.StartsWith("NV") && int.TryParse(last[2..], out int num))
            return $"NV{(num + 1):D2}";
        return "NV01";
    }

    public Task<NhanVien?> LoginAsync(string username, string password) =>
        _db.NhanViens.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

    public Task<bool> IsExistsSdtAsync(string sdt) =>
        _db.NhanViens.AnyAsync(x => x.SoDienThoai == sdt);

    public Task<bool> IsExistsUsernameAsync(string username) =>
        _db.NhanViens.AnyAsync(x => x.Username == username);
}
