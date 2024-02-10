namespace Hyperar.HPA.WinUI.ViewModels.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WinUI.Enums;

    public interface IViewModelFactory
    {
        ViewModelBase CreateMainViewModel();

        Task<ViewModelBase> CreateViewModel(ViewType viewType);
    }
}