namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using UI.Enums;
    using UI.ViewModels.Interfaces;

    public delegate Task<TViewModel> CreateAsyncViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateAsyncViewModel<AuthorizationViewModel> createAuthorizationViewModel;

        private readonly CreateAsyncViewModel<DownloadViewModel> createDownloadViewModel;

        private readonly CreateAsyncViewModel<HomeViewModel> createHomeViewModel;

        private readonly CreateAsyncViewModel<PlayersViewModel> createPlayersViewModel;

        private readonly CreateAsyncViewModel<TeamSelectionViewModel> createTeamSelectionViewModel;

        public ViewModelFactory(
            CreateAsyncViewModel<DownloadViewModel> createDownloadViewModel,
            CreateAsyncViewModel<HomeViewModel> createHomeViewModel,
            CreateAsyncViewModel<AuthorizationViewModel> createAuthorizationViewModel,
            CreateAsyncViewModel<PlayersViewModel> createPlayersViewModel,
            CreateAsyncViewModel<TeamSelectionViewModel> createTeamSelectionViewModel)
        {
            this.createDownloadViewModel = createDownloadViewModel;
            this.createAuthorizationViewModel = createAuthorizationViewModel;
            this.createPlayersViewModel = createPlayersViewModel;
            this.createHomeViewModel = createHomeViewModel;
            this.createTeamSelectionViewModel = createTeamSelectionViewModel;
        }

        public async Task<ViewModelBase> CreateAsyncViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.About => new AboutViewModel(),
                ViewType.Download => await this.createDownloadViewModel(),
                ViewType.Matches => new MatchesViewModel(),
                ViewType.Home => await this.createHomeViewModel(),
                ViewType.Authorization => await this.createAuthorizationViewModel(),
                ViewType.Players => await this.createPlayersViewModel(),
                ViewType.TeamSelection => await this.createTeamSelectionViewModel(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewType))
            };
        }
    }
}