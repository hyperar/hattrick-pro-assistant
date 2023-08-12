namespace Hyperar.HPA.UserInterface
{
    using System.IO;
    using System.Linq;
    using System.Windows;
    using Hyperar.HPA.Data;
    using Hyperar.HPA.DataContracts;
    using Hyperar.HPA.UserInterface.HostBuilders;
    using Hyperar.HPA.UserInterface.State;
    using Hyperar.HPA.UserInterface.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
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
                .AddHattrickApi()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.host.Start();

            using (var scope = this.host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
                {
                    context.Database.Migrate();
                }
            }

            var window = this.host.Services.GetRequiredService<MainWindow>();

            window.Show();

            base.OnStartup(e);
        }
    }
}
