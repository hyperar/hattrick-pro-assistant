namespace Hyperar.HPA.UI.HostBuilders
{
    using System;
    using System.Threading.Tasks;
    using Application.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using UI.State.Interfaces;
    using UI.ViewModels;
    using UI.ViewModels.Interfaces;

    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<CreateAsyncViewModel<DownloadViewModel>>(services => () => CreateDownloadAsyncViewModel(services));
                services.AddTransient<CreateAsyncViewModel<HomeViewModel>>(services => () => CreateHomeAsyncViewModel(services));
                services.AddTransient<CreateAsyncViewModel<AuthorizationViewModel>>(services => () => CreateAuthorizationAsyncViewModel(services));
                services.AddTransient<CreateAsyncViewModel<PlayersViewModel>>(services => () => CreatePlayersAsyncViewModel(services));
                services.AddTransient<CreateAsyncViewModel<TeamSelectionViewModel>>(services => () => CreateTeamSelectionAsyncViewModel(services));
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

        private static async Task<AuthorizationViewModel> CreateAuthorizationAsyncViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new AuthorizationViewModel(
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

        private static async Task<TeamSelectionViewModel> CreateTeamSelectionAsyncViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new TeamSelectionViewModel(
                scope.ServiceProvider.GetRequiredService<ITeamSelectionViewService>(),
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IViewModelFactory>());

            await viewModel.InitializeAsync();

            return viewModel;
        }
    }
}