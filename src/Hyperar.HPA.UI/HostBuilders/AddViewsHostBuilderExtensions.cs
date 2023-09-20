namespace Hyperar.HPA.UI.HostBuilders
{
    using Hyperar.HPA.UI.ViewModels;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton(s => new MainWindow(s.GetRequiredService<IConfiguration>(), s.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}