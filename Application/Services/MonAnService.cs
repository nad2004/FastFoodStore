using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class MonAnService
{
    private readonly IMonAnRepository _repo;
    public MonAnService(IMonAnRepository repo) => _repo = repo;

    public Task<List<MonAn>> GetAllAsync() => _repo.GetAllAsync();

    public async Task<(bool Success, string Message)> ThemMonAnAsync(MonAn m)
    {
        if (string.IsNullOrWhiteSpace(m.TenMon)) return (false, "Tên món không được trống");
        if (m.DonGia <= 0) return (false, "Giá phải > 0");

        m.MaMon = await _repo.GetNewIdAsync();
        bool ok = await _repo.AddAsync(m);
        return ok ? (true, "Thêm thành công") : (false, "Lỗi khi thêm");
    }

    public async Task<(bool Success, string Message)> CapNhatMonAnAsync(MonAn m)
    {
        bool ok = await _repo.UpdateAsync(m);
        return ok ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public async Task<(bool Success, string Message)> XoaMonAnAsync(string maMon)
    {
        bool ok = await _repo.DeleteAsync(maMon);
        return ok ? (true, "Xóa thành công") : (false, "Lỗi khi xóa");
    }
}
