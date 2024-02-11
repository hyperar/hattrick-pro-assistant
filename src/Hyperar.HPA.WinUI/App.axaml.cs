namespace Hyperar.HPA.WinUI
{
    using System;
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Data.Core.Plugins;
    using Avalonia.Markup.Xaml;
    using ExtensionMethods.HostBuilder;
    using Hyperar.HPA.Domain.Interfaces;
    using Hyperar.HPA.WinUI.ViewModels.Interface;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WinUI.Views;

    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var host = Host.CreateDefaultBuilder()
                .RegisterConfiguration()
                .RegisterDatabaseObjects()
                .RegisterServices()
                .RegisterStates()
                .RegisterViewModels()
                .RegisterXmlDownloadTaskExtractors()
                .RegisterXmlFileDataPersisters()
                .RegisterXmlFileParsers()
                .Build();

            host.Start();

            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IDatabaseContext>().Migrate();
            }

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);

                var mainWindow = new MainWindow();

                var viewModelFactory = host.Services.GetRequiredService<IViewModelFactory>();

                mainWindow.DataContext = viewModelFactory.CreateMainViewModel();

                desktop.MainWindow = mainWindow;
            }
        }

        public override void RegisterServices()
        {
            base.RegisterServices();
        }
    }
}