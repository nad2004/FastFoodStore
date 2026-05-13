using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class KhoService
{
    private readonly IKhoRepository _repo;
    public KhoService(IKhoRepository repo) => _repo = repo;

    public Task<List<Kho>> GetAllAsync() => _repo.GetAllAsync();

    public async Task<(bool Success, string Message)> ThemHangHoaAsync(Kho item)
    {
        if (string.IsNullOrWhiteSpace(item.TenHH)) return (false, "Tên hàng hóa không được trống");
        
        // Auto gen ID: K01, K02...
        var allIds = await _repo.GetAllMaHHAsync();
        int next = allIds.Count + 1;
        item.MaHH = $"K{next:D2}";

        bool ok = await _repo.AddAsync(item);
        return ok ? (true, "Thêm thành công") : (false, "Lỗi khi thêm");
    }

    public async Task<(bool Success, string Message)> CapNhatHangHoaAsync(Kho item)
    {
        bool ok = await _repo.UpdateAsync(item);
        return ok ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public async Task<(bool Success, string Message)> XoaHangHoaAsync(string maHH)
    {
        bool ok = await _repo.DeleteAsync(maHH);
        return ok ? (true, "Xóa thành công") : (false, "Lỗi khi xóa");
    }
}
