using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

public class KhachHangForm : UserControl
{
    private readonly KhachHangService _service;
    private DataGridView dgv = null!;
    public KhachHangForm(KhachHangService service)
    {
        _service = service;
        InitializeUI();
        _ = LoadData();
    }
    private void InitializeUI()
    {
        Dock = DockStyle.Fill;
        dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
        dgv.Columns.Add("MaKH", "Mã KH");
        dgv.Columns.Add("TenKH", "Tên KH");
        dgv.Columns.Add("Sdt", "SĐT");
        Controls.Add(dgv);
    }
    private async Task LoadData()
    {
        var data = await _service.GetAllAsync();
        dgv.Rows.Clear();
        foreach (var x in data) dgv.Rows.Add(x.MaKH, x.TenKH, x.SoDienThoai);
    }
}
