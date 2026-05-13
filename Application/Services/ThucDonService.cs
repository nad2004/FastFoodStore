using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ThucDonService
{
    private readonly IMonAnRepository _monAnRepo;
    private readonly IDanhMucRepository _dmRepo;

    public ThucDonService(IMonAnRepository monAnRepo, IDanhMucRepository dmRepo)
    {
        _monAnRepo = monAnRepo;
        _dmRepo = dmRepo;
    }

    public Task<List<MonAn>> GetAllMonAnAsync() => _monAnRepo.GetAllAsync();
    public Task<List<DanhMuc>> GetAllDanhMucAsync() => _dmRepo.GetAllAsync();

    public async Task<(bool Success, string Message)> ThemDanhMucAsync(string ma, string ten)
    {
        if (await _dmRepo.IsExistsTenDanhMucAsync(ten)) return (false, "Tên danh mục đã tồn tại");
        bool ok = await _dmRepo.InsertAsync(ma, ten);
        return ok ? (true, "Thêm thành công") : (false, "Lỗi khi thêm");
    }

    public async Task<(bool Success, string Message)> XoaDanhMucAsync(string ma)
    {
        bool ok = await _dmRepo.DeleteAsync(ma);
        return ok ? (true, "Xóa thành công") : (false, "Lỗi khi xóa");
    }
}
