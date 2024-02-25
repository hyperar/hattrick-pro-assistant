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
                services.AddTransient<CreateViewModelAsync<AboutViewModel>>(services => () => CreateAboutViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<AuthorizationViewModel>>(services => () => CreateAuthorizationViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<DownloadViewModel>>(services => () => CreateDownloadViewModelASync(services));
                services.AddTransient<CreateViewModelAsync<HomeViewModel>>(services => () => CreateHomeViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<MatchesViewModel>>(services => () => CreateMatchesViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<PlayersViewModel>>(services => () => CreatePlayersViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<SettingsViewModel>>(services => () => CreateSettingsViewModelAsync(services));
                services.AddTransient<CreateViewModelAsync<TeamSelectionViewModel>>(services => () => CreateTeamSelectionViewModelAsync(services));

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
                services.GetRequiredService<IHattrickService>(),
                scope.ServiceProvider.GetRequiredService<IUserService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<DownloadViewModel> CreateDownloadViewModelASync(IServiceProvider services)
        {
            IServiceScope scope = services.CreateScope();

            DownloadViewModel viewModel = new DownloadViewModel(
                services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IHattrickService>(),
                services.GetRequiredService<ITeamSelector>(),
                scope.ServiceProvider.GetRequiredService<IUserService>(),
                scope.ServiceProvider.GetRequiredService<IXmlFileService>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<HomeViewModel> CreateHomeViewModelAsync(IServiceProvider services)
        {
            HomeViewModel viewModel = new HomeViewModel(
                services.GetRequiredService<INavigator>());

            await viewModel.InitializeAsync();

            return viewModel;
        }

        private static async Task<MainViewModel> CreateMainViewModelAsync(IServiceProvider services)
        {
            using (IServiceScope scope = services.CreateScope())
            {
                // Scoped.
                IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                // Singleton
                IViewModelFactory viewModelFactory = services.GetRequiredService<IViewModelFactory>();
                INavigator navigator = services.GetRequiredService<INavigator>();
                ITeamSelector teamSelector = services.GetRequiredService<ITeamSelector>();

                Domain.User user = await userService.GetUserAsync();

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

                return new MainViewModel(navigator, viewModelFactory, viewType);
            }
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
            PlayersViewModel viewModel = new PlayersViewModel(
                services.GetRequiredService<INavigator>());

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
                services.GetRequiredService<ITeamSelector>());

            await viewModel.InitializeAsync();

            return viewModel;
        }
    }
}