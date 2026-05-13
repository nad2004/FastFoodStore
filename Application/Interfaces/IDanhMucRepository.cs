using Domain.Entities;

namespace Application.Interfaces;

public interface IDanhMucRepository
{
    Task<List<DanhMuc>> GetAllAsync();
    Task<bool> InsertAsync(string ma, string ten);
    Task<bool> UpdateAsync(string ma, string ten);
    Task<bool> DeleteAsync(string ma);
    Task<List<string>> GetAllTenDanhMucAsync();
    Task<bool> IsExistsTenDanhMucAsync(string ten);
}
