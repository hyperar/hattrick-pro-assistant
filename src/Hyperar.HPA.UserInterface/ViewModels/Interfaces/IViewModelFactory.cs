using Hyperar.HPA.UserInterface.Enums;

namespace Hyperar.HPA.UserInterface.ViewModels.Interfaces
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
