using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Application;
using Infrastructure;
using Serilog;
using System.IO;

namespace WinFormsApp;

static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/winforms-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var services = new ServiceCollection();
        ConfigureServices(services, configuration);

        ServiceProvider = services.BuildServiceProvider();

        using (var scope = ServiceProvider.CreateScope())
        {
            var servicesInScope = scope.ServiceProvider;
            try
            {
                Log.Information("WinForms Application starting...");
                var mainForm = servicesInScope.GetRequiredService<Form1>();
                System.Windows.Forms.Application.Run(mainForm);
                Log.Information("WinForms Application ended normally");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WinForms Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        
        // Register forms
        services.AddTransient<Form1>();
        
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
    }
}