using Domain.Entities;

namespace Application.Interfaces;

public interface INhanVienRepository
{
    Task<List<NhanVien>> GetAllAsync();
    Task<NhanVien?> GetByIdAsync(string maNV);
    Task<bool> InsertAsync(NhanVien nv);
    Task<bool> UpdateAsync(NhanVien nv);
    Task<bool> DeleteAsync(string maNV);
    Task<List<NhanVien>> SearchAsync(string keyword);
    Task<string> GetNewIdAsync();
    Task<NhanVien?> LoginAsync(string username, string password);
    Task<bool> IsExistsSdtAsync(string sdt);
    Task<bool> IsExistsUsernameAsync(string username);
}
