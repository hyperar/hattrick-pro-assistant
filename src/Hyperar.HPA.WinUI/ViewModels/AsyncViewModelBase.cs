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

        public virtual Task InitializeAsync()
        {
            this.IsInitialized = true;

            this.Navigator.ResumeNavigation();

            return Task.CompletedTask;
        }
    }
}