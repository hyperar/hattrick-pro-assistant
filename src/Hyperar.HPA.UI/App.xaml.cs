﻿namespace Hyperar.HPA.UI
{
    using System.Windows;
    using Hyperar.HPA.Domain.Interfaces;
    using Hyperar.HPA.UI.HostBuilders;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            this.host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[]? args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext()
                .AddServices()
                .AddExtractors()
                .AddParsers()
                .AddPersisters()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.host.Start();

            using (var scope = this.host.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IDatabaseContext>().Migrate();
            }

            MainWindow window = this.host.Services.GetRequiredService<MainWindow>();

            window.Show();

            base.OnStartup(e);
        }
    }
}