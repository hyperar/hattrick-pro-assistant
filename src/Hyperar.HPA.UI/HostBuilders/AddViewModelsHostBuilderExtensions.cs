namespace Hyperar.HPA.UI.HostBuilders
{
    using System;
    using System.Threading.Tasks;
    using Application.Services;
    using UI.State.Interfaces;
    using UI.ViewModels;
    using UI.ViewModels.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<CreateAsyncViewModel<DownloadViewModel>>(services => () => CreateDownloadAsyncViewModel(services));
                services.AddTransient<CreateAsyncViewModel<HomeViewModel>>(services => () => CreateHomeAsyncViewModel(services));
                services.AddTransient<CreateAsyncViewModel<PermissionsViewModel>>(services => () => CreatePermissionsAsyncViewModel(services));
                services.AddTransient<CreateAsyncViewModel<PlayersViewModel>>(services => () => CreatePlayersAsyncViewModel(services));
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return host;
        }

        private static async Task<DownloadViewModel> CreateDownloadAsyncViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new DownloadViewModel(
                scope.ServiceProvider.GetRequiredService<IAuthorizer>(),
                services.GetRequiredService<IHattrickService>(),
                scope.ServiceProvider.GetRequiredService<IUserService>(),
                scope.ServiceProvider.GetRequiredService<IXmlFileService>(),
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IViewModelFactory>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<HomeViewModel> CreateHomeAsyncViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new HomeViewModel(
                scope.ServiceProvider.GetRequiredService<IHomeViewService>(),
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<PermissionsViewModel> CreatePermissionsAsyncViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new PermissionsViewModel(
                scope.ServiceProvider.GetRequiredService<IAuthorizer>(),
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IViewModelFactory>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<PlayersViewModel> CreatePlayersAsyncViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            uint selectedTeamId = services.GetRequiredService<INavigator>().SelectedTeamId ?? 0;

            var viewModel = new PlayersViewModel(
                scope.ServiceProvider.GetRequiredService<IPlayersViewService>(),
                selectedTeamId);

            await viewModel.InitializeAsync();

            return viewModel;
        }
    }
}