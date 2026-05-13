using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<KhachHang>     KhachHangs     { get; set; }
    public DbSet<MonAn>         MonAns         { get; set; }
    public DbSet<HoaDon>        HoaDons        { get; set; }
    public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    public DbSet<Kho>           Khos           { get; set; }
    public DbSet<NhanVien>      NhanViens      { get; set; }
    public DbSet<DanhMuc>       DanhMucs       { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<KhachHang>(e =>
        {
            e.ToTable("KhachHang");
            e.HasKey(x => x.MaKH);
            e.Property(x => x.MaKH).HasMaxLength(10);
            e.Property(x => x.TenKH).HasMaxLength(100).IsRequired();
            e.Property(x => x.TheLoai).HasMaxLength(50);
            e.Property(x => x.GioiTinh).HasMaxLength(10);
            e.Property(x => x.Email).HasMaxLength(100);
            e.Property(x => x.SoDienThoai).HasMaxLength(15);
            e.Property(x => x.DiaChi).HasMaxLength(200);
        });

        modelBuilder.Entity<DanhMuc>(e =>
        {
            e.ToTable("DanhMuc");
            e.HasKey(x => x.MaDanhMuc);
            e.Property(x => x.MaDanhMuc).HasMaxLength(10);
            e.Property(x => x.TenDanhMuc).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<MonAn>(e =>
        {
            e.ToTable("MonAn");
            e.HasKey(x => x.MaMon);
            e.Property(x => x.MaMon).HasMaxLength(10);
            e.Property(x => x.TenMon).HasMaxLength(100).IsRequired();
            e.Property(x => x.DonGia).HasColumnType("numeric(18,2)");
            e.Property(x => x.DonViTinh).HasMaxLength(30);
            e.Property(x => x.HinhAnh).HasMaxLength(250);
            e.Property(x => x.TenDanhMuc).HasMaxLength(100);
            e.Property(x => x.MaDanhMuc).HasMaxLength(10);
        });

        modelBuilder.Entity<NhanVien>(e =>
        {
            e.ToTable("NhanVien");
            e.HasKey(x => x.MaNV);
            e.Property(x => x.MaNV).HasMaxLength(10);
            e.Property(x => x.TenNV).HasMaxLength(100).IsRequired();
            e.Property(x => x.NgaySinh).HasMaxLength(20);
            e.Property(x => x.GioiTinh).HasMaxLength(10);
            e.Property(x => x.ChucVu).HasMaxLength(50);
            e.Property(x => x.SoDienThoai).HasMaxLength(15);
            e.Property(x => x.DiaChi).HasMaxLength(200);
            e.Property(x => x.Username).HasMaxLength(50);
            e.Property(x => x.Password).HasMaxLength(100);
            e.Property(x => x.Role).HasMaxLength(20);
            e.HasIndex(x => x.Username).IsUnique();
        });

        modelBuilder.Entity<HoaDon>(e =>
        {
            e.ToTable("HoaDon");
            e.HasKey(x => x.MaHD);
            e.Property(x => x.MaHD).HasMaxLength(10);
            e.Property(x => x.TenNV).HasMaxLength(100);
            e.Property(x => x.TenKH).HasMaxLength(100);
            e.Property(x => x.NgayLap).HasMaxLength(20);
            e.Property(x => x.TongTien).HasColumnType("numeric(18,2)");
        });

        modelBuilder.Entity<ChiTietHoaDon>(e =>
        {
            e.ToTable("ChiTietHoaDon");
            e.HasKey(x => new { x.MaHD, x.TenMon });
            e.Property(x => x.MaHD).HasMaxLength(10);
            e.Property(x => x.TenMon).HasMaxLength(100);
            e.Property(x => x.Size).HasMaxLength(10);
            e.Property(x => x.DonGia).HasColumnType("numeric(18,2)");
            e.HasOne<HoaDon>()
             .WithMany()
             .HasForeignKey(x => x.MaHD)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Kho>(e =>
        {
            e.ToTable("Kho");
            e.HasKey(x => x.MaHH);
            e.Property(x => x.MaHH).HasMaxLength(10);
            e.Property(x => x.TenHH).HasMaxLength(100).IsRequired();
        });
    }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Try to find appsettings.json in current directory or WinFormsApp folder
        string appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        if (!File.Exists(appSettingsPath))
        {
            appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "BTL", "appsettings.json");
        }

        var config = new ConfigurationBuilder()
            .AddJsonFile(appSettingsPath, optional: false)
            .Build();

        var connStr = config.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("DefaultConnection not found.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connStr);

        return new AppDbContext(optionsBuilder.Options);
    }
}
