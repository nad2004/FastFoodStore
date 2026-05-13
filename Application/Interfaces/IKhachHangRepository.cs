using Domain.Entities;

namespace Application.Interfaces;

public interface IKhachHangRepository
{
    Task<List<KhachHang>> GetAllAsync();
    Task<KhachHang?> GetByIdAsync(string maKH);
    Task<bool> InsertAsync(KhachHang kh);
    Task<bool> UpdateAsync(KhachHang kh);
    Task<bool> DeleteAsync(string maKH);
    Task<string> GetNewIdAsync();
    Task<bool> IsExistsSdtAsync(string sdt);
    Task<int> CountAllAsync();
}
