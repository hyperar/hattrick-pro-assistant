using System;
using Hyperar.HPA.UserInterface.State.Interfaces;
using Hyperar.HPA.UserInterface.ViewModels;
using Hyperar.HPA.UserInterface.ViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperar.HPA.UserInterface.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
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

                services.AddSingleton<CreateViewModel<PermissionsViewModel>>(services => () => CreatePermissions(services));
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return host;
        }

        private static PermissionsViewModel CreatePermissions(IServiceProvider services)
        {
            return new PermissionsViewModel(
                services.GetRequiredService<IAuthorizer>());
        }
    }
}
