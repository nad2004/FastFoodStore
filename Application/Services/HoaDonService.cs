using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class HoaDonService
{
    private readonly IHoaDonRepository _repo;
    public HoaDonService(IHoaDonRepository repo) => _repo = repo;

    public Task<List<HoaDon>> GetAllAsync() => _repo.GetAllAsync();
    public Task<List<ChiTietHoaDon>> GetChiTietAsync(string maHD) => _repo.GetChiTietAsync(maHD);

    public async Task<(bool Success, string Message)> XoaHoaDonAsync(string maHD)
    {
        bool ok = await _repo.DeleteAsync(maHD);
        return ok ? (true, "Xóa thành công") : (false, "Lỗi khi xóa");
    }
}
