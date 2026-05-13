using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

public class DatMonForm : UserControl
{
    private readonly DatMonService _service;
    private readonly HoaDonService _hdService;
    public DatMonForm(DatMonService service, HoaDonService hdService)
    {
        _service = service;
        _hdService = hdService;
        InitializeUI();
    }
    private void InitializeUI()
    {
        Dock = DockStyle.Fill;
        Controls.Add(new Label { Text = "📋 Đặt Món", Font = new Font("Segoe UI", 16), Dock = DockStyle.Top });
    }
}
