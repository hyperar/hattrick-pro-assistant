namespace Hyperar.HPA.UserInterface.ViewModels.Interfaces
{
    using System.ComponentModel;

    public interface IAuthorizedViewModel : IViewModel, INotifyPropertyChanged
    {
        bool IsAuthorized { get; }

        bool IsInitialized { get; }

        void Initialize();
    }
}
