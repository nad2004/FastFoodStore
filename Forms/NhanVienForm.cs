using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

public class NhanVienForm : UserControl
{
    private readonly NhanVienService _service;
    private DataGridView dgv = null!;
    public NhanVienForm(NhanVienService service)
    {
        _service = service;
        InitializeUI();
        _ = LoadData();
    }
    private void InitializeUI()
    {
        Dock = DockStyle.Fill;
        dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
        dgv.Columns.Add("MaNV", "Mã NV");
        dgv.Columns.Add("TenNV", "Tên NV");
        dgv.Columns.Add("Role", "Quyền");
        Controls.Add(dgv);
    }
    private async Task LoadData()
    {
        var data = await _service.GetAllAsync();
        dgv.Rows.Clear();
        foreach (var x in data) dgv.Rows.Add(x.MaNV, x.TenNV, x.Role);
    }
}
