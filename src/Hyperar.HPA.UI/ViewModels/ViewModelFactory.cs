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
            switch (viewType)
            {
                case ViewType.About:
                    return new AboutViewModel();

                case ViewType.Download:
                    return await this.createDownloadViewModel();

                case ViewType.Home:
                    return await this.createHomeViewModel();

                case ViewType.Matches:
                    return new MatchesViewModel();

                case ViewType.Permissions:
                    return await this.createPermissionsViewModel();

                case ViewType.TeamSelection:
                    return new TeamSelectionViewModel();

                case ViewType.Quit:
                    return new QuitViewModel();

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType));
            }
        }
    }
}