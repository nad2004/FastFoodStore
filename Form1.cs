using Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace WinFormsApp;

public partial class Form1 : Form
{
    private readonly AppDbContext _context;
    private readonly ILogger<Form1> _logger;

    public Form1(AppDbContext context, ILogger<Form1> logger)
    {
        _context = context;
        _logger = logger;
        InitializeComponent();
        
        this.Load += Form1_Load;
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        _logger.LogInformation("Form1 loaded and DbContext injected successfully.");
        // Basic check: Try to count orders
        try 
        {
            // Note: This will fail if the database is not set up, which is expected for now.
            // But the injection itself should work.
            // int count = _context.Orders.Count();
            // MessageBox.Show($"Connected to DB. Order count: {count}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error accessing database");
        }
    }
}
