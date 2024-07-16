namespace Hyperar.HPA.WinUI
{
    using System;
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Data.Core.Plugins;
    using Avalonia.Markup.Xaml;
    using Domain.Interfaces;
    using ExtensionMethods.HostBuilder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WinUI.ViewModels.Interface;
    using WinUI.Views;

    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            IHost host = Host.CreateDefaultBuilder()
                .RegisterConfiguration()
                .RegisterDatabaseObjects()
                .RegisterServices()
                .RegisterStates()
                .RegisterViewModels()
                .RegisterFileDownloadStepProcesses()
                .Build();

            host.Start();

            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);

                SplashScreenWindow splashScreenWindow = new SplashScreenWindow();

                splashScreenWindow.Show();

                using (IServiceScope scope = host.Services.CreateScope())
                {
                    await scope.ServiceProvider.GetRequiredService<IDatabaseContext>()
                        .MigrateAsync();
                }

                desktop.MainWindow = splashScreenWindow;

                MainWindow mainWindow = new MainWindow();

                IViewModelFactory viewModelFactory = host.Services.GetRequiredService<IViewModelFactory>();

                mainWindow.DataContext = await viewModelFactory.CreateMainViewModelAsync();

                desktop.MainWindow = mainWindow;

                mainWindow.Show();
                splashScreenWindow.Close();
            }
        }

        public override void RegisterServices()
        {
            base.RegisterServices();
        }
    }
}