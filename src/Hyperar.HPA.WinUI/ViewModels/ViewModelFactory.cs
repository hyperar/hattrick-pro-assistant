namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using WinUI.Enums;
    using WinUI.ViewModels.Interface;

    internal delegate Task<TViewModel> CreateViewModelAsync<TViewModel>() where TViewModel : ViewModelBase;

    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModelAsync<AboutViewModel> createAboutViewModelAsync;

        private readonly CreateViewModelAsync<AuthorizationViewModel> createAuthorizationViewModelAsync;

        private readonly CreateViewModelAsync<DownloadViewModel> createDownloadViewModelAsync;

        private readonly CreateViewModelAsync<HomeViewModel> createHomeViewModelAsync;

        private readonly CreateViewModelAsync<JuniorMatchesViewModel> createJuniorMatchesViewModelAsync;

        private readonly CreateViewModelAsync<JuniorPlayersViewModel> createJuniorPlayersViewModelAsync;

        private readonly CreateViewModelAsync<MainViewModel> createMainViewModelAsync;

        private readonly CreateViewModelAsync<MatchesViewModel> createMatchesViewModelAsync;

        private readonly CreateViewModelAsync<PlayersViewModel> createPlayersViewModelAsync;

        private readonly CreateViewModelAsync<SettingsViewModel> createSettingsViewModelAsync;

        private readonly CreateViewModelAsync<TeamSelectionViewModel> createTeamSelectionViewModelAsync;

        public ViewModelFactory(
            CreateViewModelAsync<AboutViewModel> createAboutViewModelAsync,
            CreateViewModelAsync<AuthorizationViewModel> createAuthorizationViewModelAsync,
            CreateViewModelAsync<DownloadViewModel> createDownloadViewModelAsync,
            CreateViewModelAsync<HomeViewModel> createHomeViewModelAsync,
            CreateViewModelAsync<JuniorMatchesViewModel> createJuniorMatchesViewModelAsync,
            CreateViewModelAsync<JuniorPlayersViewModel> createJuniorPlayersViewModelAsync,
            CreateViewModelAsync<MainViewModel> createMainViewModelAsync,
            CreateViewModelAsync<MatchesViewModel> createMatchesViewModelAsync,
            CreateViewModelAsync<PlayersViewModel> createPlayersViewModelAsync,
            CreateViewModelAsync<SettingsViewModel> createSettingsViewModelAsync,
            CreateViewModelAsync<TeamSelectionViewModel> createTeamSelectionViewModelAsync)
        {
            this.createAboutViewModelAsync = createAboutViewModelAsync;
            this.createAuthorizationViewModelAsync = createAuthorizationViewModelAsync;
            this.createDownloadViewModelAsync = createDownloadViewModelAsync;
            this.createHomeViewModelAsync = createHomeViewModelAsync;
            this.createJuniorMatchesViewModelAsync = createJuniorMatchesViewModelAsync;
            this.createJuniorPlayersViewModelAsync = createJuniorPlayersViewModelAsync;
            this.createMainViewModelAsync = createMainViewModelAsync;
            this.createMatchesViewModelAsync = createMatchesViewModelAsync;
            this.createPlayersViewModelAsync = createPlayersViewModelAsync;
            this.createSettingsViewModelAsync = createSettingsViewModelAsync;
            this.createTeamSelectionViewModelAsync = createTeamSelectionViewModelAsync;
        }

        public Task<MainViewModel> CreateMainViewModelAsync()
        {
            return this.createMainViewModelAsync();
        }

        public async Task<ViewModelBase> CreateViewModelAsync(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.About => await this.createAboutViewModelAsync(),
                ViewType.Authorization => await this.createAuthorizationViewModelAsync(),
                ViewType.Download => await this.createDownloadViewModelAsync(),
                ViewType.Home => await this.createHomeViewModelAsync(),
                ViewType.JuniorMatches => await this.createJuniorMatchesViewModelAsync(),
                ViewType.JuniorPlayers => await this.createJuniorPlayersViewModelAsync(),
                ViewType.Matches => await this.createMatchesViewModelAsync(),
                ViewType.Players => await this.createPlayersViewModelAsync(),
                ViewType.Settings => await this.createSettingsViewModelAsync(),
                ViewType.TeamSelection => await this.createTeamSelectionViewModelAsync(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewType))
            };
        }
    }
}