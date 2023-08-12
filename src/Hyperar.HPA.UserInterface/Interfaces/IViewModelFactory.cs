using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyperar.HPA.UserInterface.Enums;
using Hyperar.HPA.UserInterface.ViewModels;

namespace Hyperar.HPA.UserInterface.Interfaces
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
