using System;
using Hyperar.HPA.UserInterface.Enums;
using Hyperar.HPA.UserInterface.ViewModels.Interfaces;

namespace Hyperar.HPA.UserInterface.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<PermissionsViewModel> createPermissionsViewModel;

        public ViewModelFactory(CreateViewModel<PermissionsViewModel> createPermissionsViewModel)
        {
            this.createPermissionsViewModel = createPermissionsViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return new HomeViewModel();

                case ViewType.Matches:
                    return new MatchesViewModel();

                case ViewType.About:
                    return new AboutViewModel();

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
