using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── DbContext (PostgreSQL) ──────────────────────────────────
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsql => {
                    npgsql.MigrationsAssembly("Infrastructure");
                    npgsql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }),
            ServiceLifetime.Transient,
            ServiceLifetime.Transient);

        // ── Repositories ───────────────────────────────────────────
        services.AddTransient<IKhachHangRepository, KhachHangRepository>();
        services.AddTransient<IMonAnRepository,     MonAnRepository>();
        services.AddTransient<IHoaDonRepository,    HoaDonRepository>();
        services.AddTransient<IKhoRepository,       KhoRepository>();
        services.AddTransient<INhanVienRepository,  NhanVienRepository>();
        services.AddTransient<IDanhMucRepository,   DanhMucRepository>();

        return services;
    }
}
