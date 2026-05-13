using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormsApp.Forms;

public class LoginForm : Form
{
    private readonly AuthService _authService;
    private readonly IServiceProvider _serviceProvider;
    private TextBox txtUsername = null!, txtPassword = null!;
    private Button btnLogin = null!;
    private Label lblError = null!;

    public LoginForm(AuthService authService, IServiceProvider serviceProvider)
    {
        _authService = authService;
        _serviceProvider = serviceProvider;
        InitializeUI();
    }

    private void InitializeUI()
    {
        Text = "Đăng Nhập"; Size = new Size(400, 300); StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(30, 30, 47);
        var lblTitle = new Label { Text = "🏪 ĐĂNG NHẬP", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, Location = new Point(100, 30), AutoSize = true };
        txtUsername = new TextBox { PlaceholderText = "Tài khoản", Location = new Point(50, 80), Size = new Size(300, 30) };
        txtPassword = new TextBox { PlaceholderText = "Mật khẩu", Location = new Point(50, 120), Size = new Size(300, 30), UseSystemPasswordChar = true };
        lblError = new Label { ForeColor = Color.Red, Location = new Point(50, 155), Size = new Size(300, 20) };
        btnLogin = new Button { Text = "Đăng Nhập", Location = new Point(50, 180), Size = new Size(300, 40), BackColor = Color.Blue, ForeColor = Color.White };
        btnLogin.Click += async (s, e) => {
            try {
                btnLogin.Enabled = false;
                btnLogin.Text = "Đang xử lý...";
                lblError.Text = "";
                
                var (ok, msg, user) = await _authService.LoginAsync(txtUsername.Text, txtPassword.Text);
                
                if (ok) {
                    var main = _serviceProvider.GetRequiredService<MainForm>();
                    main.SetRole(user?.Role ?? "NHÂN VIÊN");
                    main.Show(); 
                    this.Hide();
                } else {
                    lblError.Text = msg;
                    btnLogin.Enabled = true;
                    btnLogin.Text = "Đăng Nhập";
                }
            } catch (Exception ex) {
                lblError.Text = "Lỗi hệ thống: " + ex.Message;
                btnLogin.Enabled = true;
                btnLogin.Text = "Đăng Nhập";
            }
        };
        Controls.AddRange([lblTitle, txtUsername, txtPassword, lblError, btnLogin]);
    }
}
