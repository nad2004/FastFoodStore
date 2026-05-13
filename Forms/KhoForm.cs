using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

public class KhoForm : UserControl
{
    private readonly KhoService _service;
    public KhoForm(KhoService service)
    {
        _service = service;
        InitializeUI();
    }
    private void InitializeUI()
    {
        Dock = DockStyle.Fill;
        Controls.Add(new Label { Text = "📦 Quản Lý Kho", Font = new Font("Segoe UI", 16), Dock = DockStyle.Top });
    }
}
