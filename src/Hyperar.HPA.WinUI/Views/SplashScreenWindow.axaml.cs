namespace Hyperar.HPA.WinUI;
using System;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Windowing;
using Hyperar.HPA.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

public partial class SplashScreenWindow : AppWindow
{
    private readonly IServiceProvider serviceProvider;

    public SplashScreenWindow(IServiceProvider serviceProvider)
    {
        this.InitializeComponent();
        this.TitleBar.ExtendsContentIntoTitleBar = true;
        this.TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        this.serviceProvider = serviceProvider;
    }

    public async Task InitializeAsync()
    {
        using (IServiceScope scope = this.serviceProvider.CreateScope())
        {
            await scope.ServiceProvider.GetRequiredService<IDatabaseContext>().MigrateAsync();
        }
    }
}