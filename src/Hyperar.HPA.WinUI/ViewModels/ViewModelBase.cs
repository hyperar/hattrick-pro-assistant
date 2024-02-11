namespace Hyperar.HPA.WinUI.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using Hyperar.HPA.WinUI.State.Interface;

    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private INavigator navigator;

        public ViewModelBase(INavigator navigator)
        {
            this.navigator = navigator;
        }
    }
}