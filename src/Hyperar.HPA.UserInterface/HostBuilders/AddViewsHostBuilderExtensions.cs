namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using Hyperar.HPA.UserInterface.ViewModels;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<IConfiguration>(), s.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}
