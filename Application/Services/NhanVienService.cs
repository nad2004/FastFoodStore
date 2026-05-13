using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class NhanVienService
{
    private readonly INhanVienRepository _repo;
    public NhanVienService(INhanVienRepository repo) => _repo = repo;

    public Task<List<NhanVien>> GetAllAsync() => _repo.GetAllAsync();
    public Task<List<NhanVien>> SearchAsync(string keyword) => _repo.SearchAsync(keyword);

    public async Task<(bool Success, string Message)> ThemNhanVienAsync(NhanVien nv)
    {
        if (string.IsNullOrWhiteSpace(nv.TenNV)) return (false, "Tên không được trống");
        if (string.IsNullOrWhiteSpace(nv.Username)) return (false, "Username không được trống");
        if (await _repo.IsExistsUsernameAsync(nv.Username)) return (false, "Username đã tồn tại");

        nv.MaNV = await _repo.GetNewIdAsync();
        bool ok = await _repo.InsertAsync(nv);
        return ok ? (true, "Thêm thành công") : (false, "Lỗi khi thêm");
    }

    public async Task<(bool Success, string Message)> CapNhatNhanVienAsync(NhanVien nv)
    {
        bool ok = await _repo.UpdateAsync(nv);
        return ok ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public async Task<(bool Success, string Message)> XoaNhanVienAsync(string maNV)
    {
        bool ok = await _repo.DeleteAsync(maNV);
        return ok ? (true, "Xóa thành công") : (false, "Lỗi khi xóa");
    }
}
