namespace Hyperar.HPA.UserInterface.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.UserInterface.ViewModels;

    public interface INavigator
    {
        ViewModelBase? CurrentViewModel { get; set; }

        event Action? StateChanged;
    }
}
