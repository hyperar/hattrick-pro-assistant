using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyperar.HPA.UserInterface.Enums;
using Hyperar.HPA.UserInterface.Interfaces;
using Hyperar.HPA.UserInterface.ViewModels;

namespace Hyperar.HPA.UserInterface.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
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
                    return new PermissionsViewModel();
                case ViewType.Quit:
                    return new QuitViewModel();
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType));
            }
        }
    }
}
