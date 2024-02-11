namespace Hyperar.HPA.WinUI.ViewModels.Interface
{
    using System.Threading.Tasks;
    using WinUI.Enums;

    public interface IViewModelFactory
    {
        ViewModelBase CreateMainViewModel();

        Task<ViewModelBase> CreateViewModel(ViewType viewType);
    }
}