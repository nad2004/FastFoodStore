using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using WinFormsApp.Forms;

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
            .Build();

        var services = new ServiceCollection();
        ConfigureServices(services, configuration);

        ServiceProvider = services.BuildServiceProvider();

        using var scope = ServiceProvider.CreateScope();
        var sp = scope.ServiceProvider;

        System.Windows.Forms.Application.Run(sp.GetRequiredService<LoginForm>());
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddInfrastructure(configuration);
        services.AddApplication();

        // Register forms
        services.AddTransient<LoginForm>();
        services.AddTransient<MainForm>();
    }
}
