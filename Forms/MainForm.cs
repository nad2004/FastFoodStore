using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

/// <summary>
/// Form chính - MenuBar điều hướng đến các module
/// </summary>
public class MainForm : Form
{
    private readonly KhachHangService _khachHangService;
    private readonly MonAnService _monAnService;
    private readonly HoaDonService _hoaDonService;
    private readonly DatMonService _datMonService;
    private readonly KhoService _khoService;
    private readonly NhanVienService _nhanVienService;
    private readonly ThucDonService _thucDonService;
    private readonly DoanhThuService _doanhThuService;
    private readonly AuthService _authService;
    private readonly IServiceProvider _sp;

    private Panel pnlContent = null!;
    private Label lblWelcome = null!;
    private string _role = "";

    public MainForm(KhachHangService khachHangService, MonAnService monAnService,
        HoaDonService hoaDonService, DatMonService datMonService, KhoService khoService,
        NhanVienService nhanVienService, ThucDonService thucDonService,
        DoanhThuService doanhThuService, AuthService authService, IServiceProvider sp)
    {
        _khachHangService = khachHangService;
        _monAnService = monAnService;
        _hoaDonService = hoaDonService;
        _datMonService = datMonService;
        _khoService = khoService;
        _nhanVienService = nhanVienService;
        _thucDonService = thucDonService;
        _doanhThuService = doanhThuService;
        _authService = authService;
        _sp = sp;
        InitializeUI();
    }

    public void SetRole(string role)
    {
        _role = role;
        lblWelcome.Text = $"Xin chào, {Session.TenNV} ({role})";
        ApplyRolePermissions();
    }

    private void InitializeUI()
    {
        Text = "Quản Lý Cửa Hàng FastFood";
        Size = new Size(1280, 760);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(18, 18, 30);
        MinimumSize = new Size(1024, 600);

        // Sidebar
        var sidebar = new Panel
        {
            Dock = DockStyle.Left,
            Width = 200,
            BackColor = Color.FromArgb(25, 25, 40),
        };

        // Logo
        var lblLogo = new Label
        {
            Text = "🏪 FastFood",
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            ForeColor = Color.FromArgb(130, 200, 255),
            Dock = DockStyle.Top,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter,
        };

        lblWelcome = new Label
        {
            Text = "Xin chào...",
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.Gray,
            Dock = DockStyle.Top,
            Height = 30,
            TextAlign = ContentAlignment.MiddleCenter,
        };

        // Nav buttons
        string[] modules = { "🏠 Trang Chủ", "📋 Đặt Món", "🍽️ Thực Đơn", "📦 Kho", "🧾 Hóa Đơn", "👤 Khách Hàng", "👥 Nhân Viên", "📊 Doanh Thu" };
        var btnPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };

        foreach (var mod in modules)
        {
            var btn = new Button
            {
                Text = mod,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 42,
                Cursor = Cursors.Hand,
                Padding = new Padding(10, 0, 0, 0),
                Tag = mod,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => ((Button)s!).BackColor = Color.FromArgb(45, 45, 65);
            btn.MouseLeave += (s, e) => ((Button)s!).BackColor = Color.Transparent;
            btn.Click += NavBtn_Click;
            btnPanel.Controls.Add(btn);
        }

        // Logout button
        var btnLogout = new Button
        {
            Text = "🚪 Đăng Xuất",
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.Transparent,
            ForeColor = Color.OrangeRed,
            Font = new Font("Segoe UI", 10),
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Bottom,
            Height = 42,
            Cursor = Cursors.Hand,
            Padding = new Padding(10, 0, 0, 0),
        };
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.Click += (s, e) => Logout();
        sidebar.Controls.Add(btnPanel);
        sidebar.Controls.Add(lblWelcome);
        sidebar.Controls.Add(lblLogo);
        sidebar.Controls.Add(btnLogout);

        // Content area
        pnlContent = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(22, 22, 36),
            Padding = new Padding(10),
        };

        Controls.Add(pnlContent);
        Controls.Add(sidebar);

        // Show home screen by default
        ShowHomePanel();
    }

    private void NavBtn_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        var tag = btn.Tag?.ToString() ?? "";
        pnlContent.Controls.Clear();
        switch (tag)
        {
            case var t when t.Contains("Đặt Món"): pnlContent.Controls.Add(new DatMonForm(_datMonService, _hoaDonService) { Dock = DockStyle.Fill }); break;
            case var t when t.Contains("Thực Đơn"): pnlContent.Controls.Add(new ThucDonForm(_thucDonService, _monAnService) { Dock = DockStyle.Fill }); break;
            case var t when t.Contains("Kho"): pnlContent.Controls.Add(new KhoForm(_khoService) { Dock = DockStyle.Fill }); break;
            case var t when t.Contains("Hóa Đơn"): pnlContent.Controls.Add(new HoaDonForm(_hoaDonService) { Dock = DockStyle.Fill }); break;
            case var t when t.Contains("Khách Hàng"): pnlContent.Controls.Add(new KhachHangForm(_khachHangService) { Dock = DockStyle.Fill }); break;
            case var t when t.Contains("Nhân Viên"): pnlContent.Controls.Add(new NhanVienForm(_nhanVienService) { Dock = DockStyle.Fill }); break;
            case var t when t.Contains("Doanh Thu"): pnlContent.Controls.Add(new DoanhThuForm(_doanhThuService) { Dock = DockStyle.Fill }); break;
            default: ShowHomePanel(); break;
        }
    }

    private void ShowHomePanel()
    {
        pnlContent.Controls.Clear();
        var lbl = new Label
        {
            Text = "🏪 Chào mừng đến Hệ thống Quản Lý Cửa Hàng",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            ForeColor = Color.FromArgb(130, 200, 255),
            AutoSize = false,
            Dock = DockStyle.Top,
            Height = 80,
            TextAlign = ContentAlignment.MiddleCenter,
        };
        pnlContent.Controls.Add(lbl);
    }

    private void ApplyRolePermissions()
    {
        // Nhân viên không được xem module Nhân Viên
        // Logic ẩn/hiện nút sẽ được xử lý trong từng Form
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        System.Windows.Forms.Application.Exit();
    }

    private void Logout()
    {
        _authService.Logout();
        var loginForm = _sp.GetService(typeof(LoginForm)) as LoginForm;
        loginForm?.Show();
        this.Dispose(); // Dùng Dispose thay vì Close để tránh kích hoạt Application.Exit ở trên khi đăng xuất
    }
}
