namespace Hyperar.HPA.WinUI.ExtensionMethods.HostBuilder
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WinUI.Enums;
    using WinUI.State.Interface;
    using WinUI.ViewModels;
    using WinUI.ViewModels.Interface;

    public static class ViewModels
    {
        public static IHostBuilder RegisterViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<CreateViewModelAsync<AboutViewModel>>(services => () => CreateAboutViewModel(services));
                services.AddTransient<CreateViewModelAsync<AuthorizationViewModel>>(services => () => CreateAuthorizationViewModel(services));
                services.AddTransient<CreateViewModelAsync<DownloadViewModel>>(services => () => CreateDownloadViewModel(services));
                services.AddTransient<CreateViewModelAsync<HomeViewModel>>(services => () => CreateHomeViewModel(services));
                services.AddTransient<CreateViewModelAsync<MatchesViewModel>>(services => () => CreateMatchesViewModel(services));
                services.AddTransient<CreateViewModelAsync<PlayersViewModel>>(services => () => CreatePlayersViewModel(services));
                services.AddTransient<CreateViewModelAsync<SettingsViewModel>>(services => () => CreateSettingsViewModel(services));
                services.AddTransient<CreateViewModelAsync<TeamSelectionViewModel>>(services => () => CreateTeamSelectionViewModel(services));

                services.AddSingleton<CreateViewModel<MainWindowViewModel>>(services => () => CreateMainViewModel(services));
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return host;
        }

        private static async Task<AboutViewModel> CreateAboutViewModel(IServiceProvider services)
        {
            var viewModel = new AboutViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<AuthorizationViewModel> CreateAuthorizationViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new AuthorizationViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IHattrickService>(),
                scope.ServiceProvider.GetRequiredService<IUserService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<DownloadViewModel> CreateDownloadViewModel(IServiceProvider services)
        {
            var scope = services.CreateScope();

            var viewModel = new DownloadViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IHattrickService>(),
                services.GetRequiredService<ITeamSelector>(),
                scope.ServiceProvider.GetRequiredService<IUserService>(),
                scope.ServiceProvider.GetRequiredService<IXmlFileService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<HomeViewModel> CreateHomeViewModel(IServiceProvider services)
        {
            var viewModel = new HomeViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static MainWindowViewModel CreateMainViewModel(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                // Scoped.
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                // Singleton
                var viewModelFactory = services.GetRequiredService<IViewModelFactory>();
                var navigator = services.GetRequiredService<INavigator>();
                var teamSelector = services.GetRequiredService<ITeamSelector>();

                var user = userService.GetUser();

                ArgumentNullException.ThrowIfNull(user, nameof(user));

                ViewType viewType;

                if (user.Token == null)
                {
                    viewType = ViewType.Authorization;
                }
                else if (user.LastDownloadDate == null)
                {
                    viewType = ViewType.Download;
                }
                else
                {
                    ArgumentNullException.ThrowIfNull(user.Manager, nameof(user.Manager));

                    teamSelector.SetSelectedTeam(user.Manager.Teams.Single(x => x.IsPrimary).HattrickId);

                    viewType = ViewType.Home;
                }

                return new MainWindowViewModel(navigator, viewModelFactory, viewType);
            }
        }

        private static async Task<MatchesViewModel> CreateMatchesViewModel(IServiceProvider services)
        {
            var viewModel = new MatchesViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<PlayersViewModel> CreatePlayersViewModel(IServiceProvider services)
        {
            var viewModel = new PlayersViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<SettingsViewModel> CreateSettingsViewModel(IServiceProvider services)
        {
            var navigator = services.GetRequiredService<INavigator>();

            var viewModel = new SettingsViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<TeamSelectionViewModel> CreateTeamSelectionViewModel(IServiceProvider services)
        {
            var viewModel = new TeamSelectionViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<ITeamSelector>());

            await viewModel.InitializeAsync();

            return viewModel;
        }
    }
}