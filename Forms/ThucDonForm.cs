using Application.Services;
using Domain.Entities;

namespace WinFormsApp.Forms;

public class ThucDonForm : UserControl
{
    private readonly MonAnService _service;
    private DataGridView dgv = null!;
    private TextBox txtSearch = null!;
    private Button btnAdd = null!, btnEdit = null!, btnDelete = null!, btnRefresh = null!;

    public ThucDonForm(ThucDonService unused, MonAnService service) // Match MainForm call
    {
        _service = service;
        InitializeUI();
        _ = LoadData();
    }

    private void InitializeUI()
    {
        Dock = DockStyle.Fill;
        BackColor = Color.FromArgb(30, 30, 45);
        Padding = new Padding(20);

        // --- Top Panel (Title & Search) ---
        var topPanel = new Panel { Dock = DockStyle.Top, Height = 60 };
        var lblTitle = new Label { 
            Text = "🍽️ QUẢN LÝ THỰC ĐƠN", 
            Font = new Font("Segoe UI", 18, FontStyle.Bold), 
            ForeColor = Color.White, 
            AutoSize = true, 
            Location = new Point(0, 10) 
        };

        txtSearch = new TextBox { 
            PlaceholderText = "Tìm tên món...", 
            Size = new Size(250, 35), 
            Location = new Point(lblTitle.Right + 50, 15),
            BackColor = Color.FromArgb(45, 45, 60),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };
        txtSearch.TextChanged += async (s, e) => await LoadData(txtSearch.Text);

        btnAdd = CreateButton("➕ Thêm Mới", Color.FromArgb(40, 167, 69), new Point(txtSearch.Right + 20, 12));
        btnRefresh = CreateButton("🔄 Làm Mới", Color.FromArgb(0, 123, 255), new Point(btnAdd.Right + 10, 12));

        topPanel.Controls.AddRange([lblTitle, txtSearch, btnAdd, btnRefresh]);

        // --- Grid Data ---
        dgv = new DataGridView { 
            Dock = DockStyle.Fill, 
            BackgroundColor = Color.FromArgb(30, 30, 45),
            BorderStyle = BorderStyle.None,
            ForeColor = Color.White,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AllowUserToAddRows = false,
            ReadOnly = true,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToResizeRows = false
        };

        dgv.EnableHeadersVisualStyles = false;
        dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle {
            BackColor = Color.FromArgb(50, 50, 70),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };

        dgv.DefaultCellStyle = new DataGridViewCellStyle {
            BackColor = Color.FromArgb(40, 40, 55),
            ForeColor = Color.White,
            SelectionBackColor = Color.FromArgb(0, 120, 215),
            Font = new Font("Segoe UI", 10)
        };

        dgv.Columns.Add("MaMon", "Mã Món");
        dgv.Columns.Add("TenMon", "Tên Món Ăn");
        dgv.Columns.Add("DonGia", "Đơn Giá");
        dgv.Columns.Add("DonViTinh", "ĐVT");
        dgv.Columns.Add("MaDM", "Mã DM");

        // --- Bottom Panel (Actions) ---
        var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 60 };
        btnEdit = CreateButton("✏️ Chỉnh Sửa", Color.FromArgb(255, 193, 7), new Point(0, 15));
        btnDelete = CreateButton("🗑️ Xóa Món", Color.FromArgb(220, 53, 69), new Point(btnEdit.Right + 10, 15));
        btnEdit.ForeColor = Color.Black;
        bottomPanel.Controls.AddRange([btnEdit, btnDelete]);

        // --- Events ---
        btnAdd.Click += async (s, e) => {
            var diag = new MonAnDialog();
            if (diag.ShowDialog() == DialogResult.OK) {
                var (ok, msg) = await _service.ThemMonAnAsync(diag.MonAn);
                MessageBox.Show(msg);
                await LoadData();
            }
        };

        btnEdit.Click += async (s, e) => {
            if (dgv.SelectedRows.Count == 0) return;
            var ma = dgv.SelectedRows[0].Cells["MaMon"].Value.ToString();
            var items = await _service.GetAllAsync();
            var item = items.FirstOrDefault(x => x.MaMon == ma);
            if (item != null) {
                var diag = new MonAnDialog(item);
                if (diag.ShowDialog() == DialogResult.OK) {
                    var (ok, msg) = await _service.CapNhatMonAnAsync(diag.MonAn);
                    MessageBox.Show(msg);
                    await LoadData();
                }
            }
        };

        btnDelete.Click += async (s, e) => {
            if (dgv.SelectedRows.Count == 0) return;
            var ma = dgv.SelectedRows[0].Cells["MaMon"].Value.ToString();
            if (MessageBox.Show("Bạn có chắc muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                var (ok, msg) = await _service.XoaMonAnAsync(ma);
                MessageBox.Show(msg);
                await LoadData();
            }
        };

        btnRefresh.Click += async (s, e) => await LoadData();

        Controls.AddRange([dgv, topPanel, bottomPanel]);
    }

    private Button CreateButton(string text, Color color, Point pos)
    {
        return new Button {
            Text = text,
            BackColor = color,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(120, 35),
            Location = pos,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
    }

    private async Task LoadData(string search = "")
    {
        var data = await _service.GetAllAsync();
        if (!string.IsNullOrEmpty(search))
            data = data.Where(x => x.TenMon.ToLower().Contains(search.ToLower())).ToList();

        dgv.Rows.Clear();
        foreach (var x in data)
            dgv.Rows.Add(x.MaMon, x.TenMon, x.DonGia.ToString("N0"), x.DonViTinh, x.MaDanhMuc);
    }
}
