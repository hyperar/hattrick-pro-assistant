namespace Hyperar.HPA.UI.State
{
    using System;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;

    public class Navigator : INavigator
    {
        private bool canNavigate;

        private ViewModelBase? currentViewModel;

        public Navigator()
        {
            this.canNavigate = true;
        }

        public event Action? StateChanged;

        public bool CanNavigate
        {
            get
            {
                return this.canNavigate;
            }
            set
            {
                this.canNavigate = value;

                this.StateChanged?.Invoke();
            }
        }

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