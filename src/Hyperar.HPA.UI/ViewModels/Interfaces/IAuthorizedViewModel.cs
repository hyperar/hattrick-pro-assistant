namespace Hyperar.HPA.UI.ViewModels.Interfaces
{
    using System.ComponentModel;

    public interface IAuthorizedViewModel : IViewModel, INotifyPropertyChanged
    {
        bool? IsAuthorized { get; }

        bool? IsNotAuthorized { get; }
    }
}