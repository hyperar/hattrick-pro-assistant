namespace Hyperar.HPA.UserInterface.State
{
    using System;
    using Hyperar.HPA.UserInterface.State.Interfaces;
    using Hyperar.HPA.UserInterface.ViewModels;

    public class Navigator : INavigator
    {
        private ViewModelBase? currentViewModel;
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

        public event Action? StateChanged;
    }
}
