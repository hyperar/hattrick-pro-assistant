namespace Hyperar.HPA.UserInterface.ViewModels
{
    using System;
    using Hyperar.HPA.UserInterface.Enums;
    using Hyperar.HPA.UserInterface.ViewModels.Interfaces;

    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<PermissionsViewModel> createPermissionsViewModel;
        private readonly CreateViewModel<DownloadViewModel> createDownloadViewModel;

        public ViewModelFactory(
            CreateViewModel<DownloadViewModel> createDownloadViewModel,
            CreateViewModel<PermissionsViewModel> createPermissionsViewModel)
        {
            this.createPermissionsViewModel = createPermissionsViewModel;
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
                    return new HomeViewModel();

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
