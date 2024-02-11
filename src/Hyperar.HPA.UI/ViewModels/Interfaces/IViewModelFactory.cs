namespace Hyperar.HPA.UI.ViewModels.Interfaces
{
    using System.Threading.Tasks;
    using UI.Enums;

    public interface IViewModelFactory
    {
        Task<ViewModelBase> CreateAsyncViewModel(ViewType viewType);
    }
}