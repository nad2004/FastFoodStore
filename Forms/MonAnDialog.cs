using Domain.Entities;

namespace WinFormsApp.Forms;

public class MonAnDialog : Form
{
    public MonAn MonAn { get; private set; }
    private TextBox txtTen = null!, txtGia = null!, txtDvt = null!, txtMaDM = null!;
    private Button btnSave = null!, btnCancel = null!;

    public MonAnDialog(MonAn? existing = null)
    {
        MonAn = existing ?? new MonAn();
        InitializeUI();
        if (existing != null)
        {
            txtTen.Text = existing.TenMon;
            txtGia.Text = existing.DonGia.ToString();
            txtDvt.Text = existing.DonViTinh;
            txtMaDM.Text = existing.MaDanhMuc;
        }
    }

    private void InitializeUI()
    {
        Text = MonAn.MaMon == null ? "Thêm Món Ăn Mới" : "Chỉnh Sửa Món Ăn";
        Size = new Size(400, 450); StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
        BackColor = Color.FromArgb(32, 33, 36); ForeColor = Color.White;

        var lblTitle = new Label { Text = Text.ToUpper(), Font = new Font("Segoe UI", 14, FontStyle.Bold), Dock = DockStyle.Top, Height = 50, TextAlign = ContentAlignment.MiddleCenter };

        var pnl = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
        
        int y = 10;
        AddLabelAndControl(pnl, "Tên món ăn:", txtTen = new TextBox(), ref y);
        AddLabelAndControl(pnl, "Đơn giá:", txtGia = new TextBox(), ref y);
        AddLabelAndControl(pnl, "Đơn vị tính:", txtDvt = new TextBox(), ref y);
        AddLabelAndControl(pnl, "Mã danh mục:", txtMaDM = new TextBox(), ref y);

        btnSave = new Button { Text = "LƯU", Size = new Size(100, 40), Location = new Point(160, y + 20), BackColor = Color.FromArgb(0, 120, 215), FlatStyle = FlatStyle.Flat };
        btnCancel = new Button { Text = "HỦY", Size = new Size(100, 40), Location = new Point(270, y + 20), BackColor = Color.DimGray, FlatStyle = FlatStyle.Flat };

        btnSave.Click += (s, e) => {
            if (decimal.TryParse(txtGia.Text, out decimal gia))
            {
                MonAn.TenMon = txtTen.Text;
                MonAn.DonGia = gia;
                MonAn.DonViTinh = txtDvt.Text;
                MonAn.MaDanhMuc = txtMaDM.Text;
                DialogResult = DialogResult.OK;
            }
            else MessageBox.Show("Giá tiền không hợp lệ!");
        };
        btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

        pnl.Controls.AddRange([btnSave, btnCancel]);
        Controls.AddRange([pnl, lblTitle]);
    }

    private void AddLabelAndControl(Panel p, string label, Control c, ref int y)
    {
        p.Controls.Add(new Label { Text = label, Location = new Point(10, y), AutoSize = true });
        c.Location = new Point(10, y + 20); c.Size = new Size(340, 30);
        c.BackColor = Color.FromArgb(45, 45, 48); c.ForeColor = Color.White;
        p.Controls.Add(c);
        y += 60;
    }
}
