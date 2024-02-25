namespace Hyperar.HPA.WinUI.ViewModels.Interface
{
    using System.Threading.Tasks;
    using WinUI.Enums;

    public interface IViewModelFactory
    {
        Task<MainViewModel> CreateMainViewModelAsync();

        Task<ViewModelBase> CreateViewModelAsync(ViewType viewType);
    }
}