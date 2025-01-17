using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using UI.Repository;

namespace UI;
public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        services.AddSingleton<ApiClient>();
        services.AddSingleton<IAttributeRepository, AttributeRepository>();
        ServiceProvider = services.BuildServiceProvider();
    }
}