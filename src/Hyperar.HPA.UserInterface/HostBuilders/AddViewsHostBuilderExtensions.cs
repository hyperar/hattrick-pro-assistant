namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.UserInterface.Interfaces;
    using Hyperar.HPA.UserInterface.State;
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
                services.AddSingleton<INavigator, Navigator>();
            });

            return host;
        }
    }
}
