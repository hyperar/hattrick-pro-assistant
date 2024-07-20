namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using System;
    using System.Threading.Tasks;
    using Application.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WinUI.State.Interface;
    using WinUI.ViewModels;
    using WinUI.ViewModels.Interface;

    public static class ViewModels
    {
        public static IHostBuilder RegisterViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<CreateViewModelAsync<AboutViewModel>>(services => () => CreateAboutViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<AuthorizationViewModel>>(services => () => CreateAuthorizationViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<DownloadViewModel>>(services => () => CreateDownloadViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<HomeViewModel>>(services => () => CreateHomeViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<MatchesViewModel>>(services => () => CreateMatchesViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<PlayersViewModel>>(services => () => CreatePlayersViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<SettingsViewModel>>(services => () => CreateSettingsViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<TeamSelectionViewModel>>(services => () => CreateTeamSelectionViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<JuniorMatchesViewModel>>(services => () => CreateJuniorMatchesViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<JuniorPlayersViewModel>>(services => () => CreateJuniorPlayersViewModelAsync(services));

                services.AddSingleton<CreateViewModelAsync<MainViewModel>>(services => () => CreateMainViewModelAsync(services));
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return host;
        }

        private static async Task<AboutViewModel> CreateAboutViewModelAsync(IServiceProvider services)
        {
            AboutViewModel viewModel = new AboutViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<AuthorizationViewModel> CreateAuthorizationViewModelAsync(IServiceProvider services)
        {
            IServiceScope scope = services.CreateScope();

            AuthorizationViewModel viewModel = new AuthorizationViewModel(
                services.GetRequiredService<INavigator>(),
                scope.ServiceProvider.GetRequiredService<IHattrickService>(),
                scope.ServiceProvider.GetRequiredService<IUserService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<DownloadViewModel> CreateDownloadViewModelAsync(IServiceProvider services)
        {
            IServiceScope scope = services.CreateScope();

            DownloadViewModel viewModel = new DownloadViewModel(
                services.GetRequiredService<INavigator>(),
                scope.ServiceProvider.GetRequiredService<IDownloadViewService>(),
                scope.ServiceProvider.GetRequiredService<IUserService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<HomeViewModel> CreateHomeViewModelAsync(IServiceProvider services)
        {
            HomeViewModel viewModel = new HomeViewModel(
                services.GetRequiredService<INavigator>(),
                services.CreateScope().ServiceProvider.GetRequiredService<IHomeViewService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<JuniorMatchesViewModel> CreateJuniorMatchesViewModelAsync(IServiceProvider services)
        {
            JuniorMatchesViewModel viewModel = new JuniorMatchesViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<JuniorPlayersViewModel> CreateJuniorPlayersViewModelAsync(IServiceProvider services)
        {
            IServiceScope scope = services.CreateScope();

            JuniorPlayersViewModel viewModel = new JuniorPlayersViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<MainViewModel> CreateMainViewModelAsync(IServiceProvider services)
        {
            // Singleton
            IViewModelFactory viewModelFactory = services.GetRequiredService<IViewModelFactory>();
            INavigator navigator = services.GetRequiredService<INavigator>();
            IServiceScopeFactory serviceScopeFactory = services.GetRequiredService<IServiceScopeFactory>();

            var viewModel = new MainViewModel(navigator, serviceScopeFactory, viewModelFactory);

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<MatchesViewModel> CreateMatchesViewModelAsync(IServiceProvider services)
        {
            MatchesViewModel viewModel = new MatchesViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<PlayersViewModel> CreatePlayersViewModelAsync(IServiceProvider services)
        {
            IServiceScope scope = services.CreateScope();

            PlayersViewModel viewModel = new PlayersViewModel(
                services.GetRequiredService<INavigator>(),
                scope.ServiceProvider.GetRequiredService<IPlayersViewService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<SettingsViewModel> CreateSettingsViewModelAsync(IServiceProvider services)
        {
            INavigator navigator = services.GetRequiredService<INavigator>();

            SettingsViewModel viewModel = new SettingsViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<TeamSelectionViewModel> CreateTeamSelectionViewModelAsync(IServiceProvider services)
        {
            TeamSelectionViewModel viewModel = new TeamSelectionViewModel(
                services.GetRequiredService<INavigator>(),
                services.CreateScope().ServiceProvider.GetRequiredService<ITeamSelectionViewService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }
    }
}