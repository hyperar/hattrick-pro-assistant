using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyperar.HPA.UserInterface.Factories;
using Hyperar.HPA.UserInterface.Interfaces;
using Hyperar.HPA.UserInterface.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperar.HPA.UserInterface.HostBuilders
{
    static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<AboutViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<MainViewModel>();
                services.AddTransient<MatchesViewModel>();
                services.AddTransient<PermissionsViewModel>();
                services.AddTransient<QuitViewModel>();

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return host;
        }
    }
}
