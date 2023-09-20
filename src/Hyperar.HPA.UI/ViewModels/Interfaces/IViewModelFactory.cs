namespace Hyperar.HPA.UI.ViewModels.Interfaces
{
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.ViewModels;

    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}