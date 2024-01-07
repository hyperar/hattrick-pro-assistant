namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public delegate Task<TViewModel> CreateAsyncViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateAsyncViewModel<DownloadViewModel> createDownloadViewModel;

        private readonly CreateAsyncViewModel<HomeViewModel> createHomeViewModel;

        private readonly CreateAsyncViewModel<PermissionsViewModel> createPermissionsViewModel;

        private readonly CreateAsyncViewModel<PlayersViewModel> createPlayersViewModel;

        public ViewModelFactory(
            CreateAsyncViewModel<DownloadViewModel> createDownloadViewModel,
            CreateAsyncViewModel<HomeViewModel> createHomeViewModel,
            CreateAsyncViewModel<PermissionsViewModel> createPermissionsViewModel,
            CreateAsyncViewModel<PlayersViewModel> createPlayersViewModel)
        {
            this.createDownloadViewModel = createDownloadViewModel;
            this.createPermissionsViewModel = createPermissionsViewModel;
            this.createPlayersViewModel = createPlayersViewModel;
            this.createHomeViewModel = createHomeViewModel;
        }

        public async Task<ViewModelBase> CreateAsyncViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.About => new AboutViewModel(),
                ViewType.Download => await this.createDownloadViewModel(),
                ViewType.Matches => new MatchesViewModel(),
                ViewType.Home => await this.createHomeViewModel(),
                ViewType.Permissions => await this.createPermissionsViewModel(),
                ViewType.Players => await this.createPlayersViewModel(),
                ViewType.TeamSelection => new TeamSelectionViewModel(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewType))
            };
        }
    }
}