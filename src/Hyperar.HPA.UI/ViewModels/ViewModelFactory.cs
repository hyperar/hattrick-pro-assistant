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

        public ViewModelFactory(
            CreateAsyncViewModel<DownloadViewModel> createDownloadViewModel,
            CreateAsyncViewModel<HomeViewModel> createHomeViewModel,
            CreateAsyncViewModel<PermissionsViewModel> createPermissionsViewModel)
        {
            this.createDownloadViewModel = createDownloadViewModel;
            this.createHomeViewModel = createHomeViewModel;
            this.createPermissionsViewModel = createPermissionsViewModel;
        }

        public async Task<ViewModelBase> CreateAsyncViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.About => new AboutViewModel(),
                ViewType.Download => await this.createDownloadViewModel(),
                ViewType.Home => await this.createHomeViewModel(),
                ViewType.Matches => new MatchesViewModel(),
                ViewType.Permissions => await this.createPermissionsViewModel(),
                ViewType.TeamSelection => new TeamSelectionViewModel(),
                ViewType.Quit => new QuitViewModel(),
                _ => throw new ArgumentOutOfRangeException(nameof(viewType))
            };
        }
    }
}