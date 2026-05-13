using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class DoanhThuService
{
    private readonly IHoaDonRepository _hdRepo;
    public DoanhThuService(IHoaDonRepository hdRepo) => _hdRepo = hdRepo;

    public async Task<(List<ThongKeDoanhThu> Data, decimal Tong, string? Error)> GetDoanhThuTheoKhoangThoiGianAsync(string from, string to)
    {
        try
        {
            var data = await _hdRepo.GetDoanhThuTheoNgayAsync(from, to);
            decimal tong = data.Sum(x => x.TongDoanhThu);
            return (data, tong, null);
        }
        catch (Exception ex)
        {
            return ([], 0, ex.Message);
        }
    }
}
