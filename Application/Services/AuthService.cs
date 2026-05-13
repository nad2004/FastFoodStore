using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class AuthService
{
    private readonly INhanVienRepository _repo;
    public AuthService(INhanVienRepository repo) => _repo = repo;

    public async Task<(bool Success, string Message, NhanVien? User)> LoginAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return (false, "Vui lòng nhập tài khoản và mật khẩu", null);

        var user = await _repo.LoginAsync(username, password);
        if (user != null)
        {
            Session.MaNV = user.MaNV;
            Session.TenNV = user.TenNV;
            return (true, "Đăng nhập thành công", user);
        }
        return (false, "Sai tài khoản hoặc mật khẩu", null);
    }

    public void Logout()
    {
        Session.MaNV = null;
        Session.TenNV = null;
    }
}
