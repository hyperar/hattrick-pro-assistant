namespace Hyperar.HPA.UI.ViewModels.Interfaces
{
    using System.ComponentModel;
    using System.Threading.Tasks;

    public interface IViewModel : INotifyPropertyChanged
    {
        bool IsInitialized { get; set; }

        Task InitializeAsync();
    }
}