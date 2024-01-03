namespace Hyperar.HPA.UI.ViewModels.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsInitialized { get; set; }

        Task InitializeAsync();
    }
}