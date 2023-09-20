namespace Hyperar.HPA.UI.State
{
    using System;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;

    public class Navigator : INavigator
    {
        private ViewModelBase? currentViewModel;

        public event Action? StateChanged;

        public ViewModelBase? CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.currentViewModel?.Dispose();

                this.currentViewModel = value;
                this.StateChanged?.Invoke();
            }
        }
    }
}