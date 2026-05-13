using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Dependency Injection extension methods for Application layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Đăng ký tất cả Service của Application layer
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<KhachHangService>();
        services.AddTransient<MonAnService>();
        services.AddTransient<HoaDonService>();
        services.AddTransient<DatMonService>();
        services.AddTransient<KhoService>();
        services.AddTransient<NhanVienService>();
        services.AddTransient<ThucDonService>();
        services.AddTransient<DoanhThuService>();
        services.AddTransient<AuthService>();

        return services;
    }
}
