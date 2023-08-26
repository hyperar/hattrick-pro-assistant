namespace Hyperar.HPA.UserInterface.ViewModels.Interfaces
{
    using Hyperar.HPA.UserInterface.Enums;

    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
