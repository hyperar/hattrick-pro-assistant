namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using WinUI.Enums;
    using WinUI.ViewModels.Interface;

    internal delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    internal delegate Task<TViewModel> CreateViewModelAsync<TViewModel>() where TViewModel : ViewModelBase;

    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModelAsync<AboutViewModel> createAboutViewModel;

        private readonly CreateViewModelAsync<AuthorizationViewModel> createAuthorizationViewModel;

        private readonly CreateViewModelAsync<DownloadViewModel> createDownloadViewModel;

        private readonly CreateViewModelAsync<HomeViewModel> createHomeViewModel;

        private readonly CreateViewModel<MainWindowViewModel> createMainViewModel;

        private readonly CreateViewModelAsync<MatchesViewModel> createMatchesViewModel;

        private readonly CreateViewModelAsync<PlayersViewModel> createPlayersViewModel;

        private readonly CreateViewModelAsync<SettingsViewModel> createSettingsViewModel;

        private readonly CreateViewModelAsync<TeamSelectionViewModel> createTeamSelectionViewModel;

        public ViewModelFactory(
            CreateViewModelAsync<AboutViewModel> createAboutViewModel,
            CreateViewModelAsync<AuthorizationViewModel> createAuthorizationViewModel,
            CreateViewModelAsync<DownloadViewModel> createDownloadViewModel,
            CreateViewModelAsync<HomeViewModel> createHomeViewModel,
            CreateViewModel<MainWindowViewModel> createMainViewModel,
            CreateViewModelAsync<MatchesViewModel> createMatchesViewModel,
            CreateViewModelAsync<PlayersViewModel> createPlayersViewModel,
            CreateViewModelAsync<SettingsViewModel> createSettingsViewModel,
            CreateViewModelAsync<TeamSelectionViewModel> createTeamSelectionViewModel)
        {
            this.createAboutViewModel = createAboutViewModel;
            this.createAuthorizationViewModel = createAuthorizationViewModel;
            this.createDownloadViewModel = createDownloadViewModel;
            this.createHomeViewModel = createHomeViewModel;
            this.createMainViewModel = createMainViewModel;
            this.createMatchesViewModel = createMatchesViewModel;
            this.createPlayersViewModel = createPlayersViewModel;
            this.createSettingsViewModel = createSettingsViewModel;
            this.createTeamSelectionViewModel = createTeamSelectionViewModel;
        }

        public ViewModelBase CreateMainViewModel()
        {
            return this.createMainViewModel();
        }

        public async Task<ViewModelBase> CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.About => await this.createAboutViewModel(),
                ViewType.Authorization => await this.createAuthorizationViewModel(),
                ViewType.Download => await this.createDownloadViewModel(),
                ViewType.Home => await this.createHomeViewModel(),
                ViewType.Matches => await this.createMatchesViewModel(),
                ViewType.Players => await this.createPlayersViewModel(),
                ViewType.Settings => await this.createSettingsViewModel(),
                ViewType.TeamSelection => await this.createTeamSelectionViewModel(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewType))
            };
        }
    }
}