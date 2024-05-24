namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using WinUI.State.Interface;

    public partial class AsyncViewModelBase : ViewModelBase, INotifyPropertyChanged
    {
        [ObservableProperty]
        private bool isInitialized;

        public AsyncViewModelBase(INavigator navigator) : base(navigator)
        {
            this.IsInitialized = false;
        }

        public virtual async Task InitializeAsync()
        {
            await Task.Run(() => this.IsInitialized = true);
        }
    }
}