namespace Hyperar.HPA.UserInterface
{
    using System.Windows;
    using Hyperar.HPA.Data;
    using Hyperar.HPA.UserInterface.HostBuilders;
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
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.host.Start();

            //using (var stream = File.OpenRead("C:\\Users\\matia\\Desktop\\TeamDetails_2.xml"))
            //{
            //    var result = this.host.Services.GetRequiredService<IXmlFileService>().ParseFile(stream);
            //}

            using (var context = this.host.Services.GetRequiredService<DatabaseContextFactory>().CreateDbContext())
            {
                context.Database.Migrate();
            }

            var window = this.host.Services.GetRequiredService<MainWindow>();

            window.Show();

            base.OnStartup(e);
        }
    }
}
