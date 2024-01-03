namespace Hyperar.HPA.UI.ViewModels.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.UI.Enums;

    public interface IViewModelFactory
    {
        Task<ViewModelBase> CreateAsyncViewModel(ViewType viewType);
    }
}