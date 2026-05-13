using Domain.Entities;

namespace Application.Interfaces;

public interface IMonAnRepository
{
    Task<List<MonAn>> GetAllAsync();
    Task<MonAn?> GetByIdAsync(string maMon);
    Task<bool> AddAsync(MonAn m);
    Task<bool> UpdateAsync(MonAn m);
    Task<bool> DeleteAsync(string maMon);
    Task<string> GetNewIdAsync();
    Task<bool> IsExistsTenMonAsync(string tenMon);
    Task<int> CountAllAsync();
}
