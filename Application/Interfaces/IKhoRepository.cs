using Domain.Entities;

namespace Application.Interfaces;

public interface IKhoRepository
{
    Task<List<Kho>> GetAllAsync();
    Task<Kho?> GetByIdAsync(string maHH);
    Task<bool> AddAsync(Kho item);
    Task<bool> UpdateAsync(Kho item);
    Task<bool> DeleteAsync(string maHH);
    Task<List<string>> GetAllMaHHAsync();
    Task<int> GetSoLuongTonAsync(string maHH);
    Task TruKhoAsync(string maHH, int soLuongMua);
}
