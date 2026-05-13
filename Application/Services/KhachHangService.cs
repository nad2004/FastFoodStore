using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class KhachHangService
{
    private readonly IKhachHangRepository _repo;
    public KhachHangService(IKhachHangRepository repo) => _repo = repo;

    public Task<List<KhachHang>> GetAllAsync() => _repo.GetAllAsync();

    public async Task<(bool Success, string Message)> ThemKhachHangAsync(KhachHang kh)
    {
        if (string.IsNullOrWhiteSpace(kh.TenKH)) return (false, "Tên khách hàng không được để trống");
        if (string.IsNullOrWhiteSpace(kh.SoDienThoai)) return (false, "SĐT không được để trống");
        if (await _repo.IsExistsSdtAsync(kh.SoDienThoai)) return (false, "SĐT đã tồn tại");

        kh.MaKH = await _repo.GetNewIdAsync();
        bool ok = await _repo.InsertAsync(kh);
        return ok ? (true, "Thêm thành công") : (false, "Lỗi khi thêm");
    }

    public async Task<(bool Success, string Message)> CapNhatKhachHangAsync(KhachHang kh)
    {
        bool ok = await _repo.UpdateAsync(kh);
        return ok ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public async Task<(bool Success, string Message)> XoaKhachHangAsync(string maKH)
    {
        bool ok = await _repo.DeleteAsync(maKH);
        return ok ? (true, "Xóa thành công") : (false, "Lỗi khi xóa");
    }
}
