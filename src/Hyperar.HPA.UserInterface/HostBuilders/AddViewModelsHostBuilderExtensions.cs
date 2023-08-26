namespace Hyperar.HPA.UserInterface.HostBuilders
{
    using System;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.UserInterface.State.Interfaces;
    using Hyperar.HPA.UserInterface.ViewModels;
    using Hyperar.HPA.UserInterface.ViewModels.Interfaces;
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
                services.AddTransient<CreateViewModel<PermissionsViewModel>>(services => () => CreatePermissionsViewModel(services));
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return host;
        }

        private static DownloadViewModel CreateDownloadViewModel(IServiceProvider services)
        {
            return new DownloadViewModel(
                services.GetRequiredService<IAuthorizer>(),
                services.GetRequiredService<IHattrickService>(),
                services.GetRequiredService<IXmlFileService>());
        }

        private static PermissionsViewModel CreatePermissionsViewModel(IServiceProvider services)
        {
            return new PermissionsViewModel(
                services.GetRequiredService<IAuthorizer>());
        }
    }
}
