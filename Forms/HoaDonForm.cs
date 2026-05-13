using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

public class HoaDonForm : UserControl
{
    private readonly HoaDonService _service;
    private DataGridView dgv = null!;
    public HoaDonForm(HoaDonService service)
    {
        _service = service;
        InitializeUI();
        _ = LoadData();
    }
    private void InitializeUI()
    {
        Dock = DockStyle.Fill;
        dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
        dgv.Columns.Add("MaHD", "Mã HĐ");
        dgv.Columns.Add("Khach", "Khách");
        dgv.Columns.Add("Tong", "Tổng Tiền");
        Controls.Add(dgv);
    }
    private async Task LoadData()
    {
        var data = await _service.GetAllAsync();
        dgv.Rows.Clear();
        foreach (var x in data) dgv.Rows.Add(x.MaHD, x.TenKH, x.TongTien);
    }
}
