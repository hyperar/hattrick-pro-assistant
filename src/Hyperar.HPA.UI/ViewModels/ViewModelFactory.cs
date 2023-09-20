namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<DownloadViewModel> createDownloadViewModel;

        private readonly CreateViewModel<HomeViewModel> createHomeViewModel;

        private readonly CreateViewModel<PermissionsViewModel> createPermissionsViewModel;

        public ViewModelFactory(
            CreateViewModel<DownloadViewModel> createDownloadViewModel,
            CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<PermissionsViewModel> createPermissionsViewModel)
        {
            this.createPermissionsViewModel = createPermissionsViewModel;
            this.createHomeViewModel = createHomeViewModel;
            this.createDownloadViewModel = createDownloadViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.About:
                    return new AboutViewModel();

                case ViewType.Download:
                    return this.createDownloadViewModel();

                case ViewType.Home:
                    return this.createHomeViewModel();

                case ViewType.Matches:
                    return new MatchesViewModel();

                case ViewType.Permissions:
                    return this.createPermissionsViewModel();

                case ViewType.Quit:
                    return new QuitViewModel();

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType));
            }
        }
    }
}