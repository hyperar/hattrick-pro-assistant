namespace Hyperar.HPA.UI.HostBuilders
{
    using System;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;
    using Hyperar.HPA.UI.ViewModels.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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

                services.AddTransient<CreateViewModel<DownloadViewModel>>(services => () => CreateDownloadViewModel(services));
                services.AddTransient<CreateViewModel<HomeViewModel>>(services => () => CreateHomeViewModel(services));
                services.AddTransient<CreateViewModel<PermissionsViewModel>>(services => () => CreatePermissionsViewModel(services));
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return host;
        }

        private static DownloadViewModel CreateDownloadViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            return new DownloadViewModel(
                scope.ServiceProvider.GetRequiredService<IAuthorizer>(),
                services.GetRequiredService<IHattrickService>(),
                scope.ServiceProvider.GetRequiredService<IXmlFileService>());
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            return new HomeViewModel(
                scope.ServiceProvider.GetRequiredService<IHomeViewService>());
        }

        private static PermissionsViewModel CreatePermissionsViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            return new PermissionsViewModel(
                scope.ServiceProvider.GetRequiredService<IAuthorizer>());
        }
    }
}