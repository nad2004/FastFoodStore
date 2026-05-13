using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

public class DoanhThuForm : UserControl
{
    private readonly DoanhThuService _service;
    public DoanhThuForm(DoanhThuService service)
    {
        _service = service;
        InitializeUI();
    }
    private void InitializeUI()
    {
        Dock = DockStyle.Fill;
        Controls.Add(new Label { Text = "📊 Thống Kê Doanh Thu", Font = new Font("Segoe UI", 16), Dock = DockStyle.Top });
    }
}
