namespace Hyperar.HPA.WinUI.State
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using WinUI.State.Interface;
    using WinUI.ViewModels;

    public class Navigator : ObservableObject, INavigator
    {
        private bool canNavigate;

        private ViewModelBase? currentPage;

        public event Action? StateChanged;

        public bool CanNavigate
        {
            get
            {
                return this.canNavigate;
            }
            private set
            {
                this.canNavigate = value;

                this.StateChanged?.Invoke();
            }
        }

        public ViewModelBase? CurrentPage
        {
            get
            {
                return this.currentPage;
            }

            set
            {
                this.currentPage = value;

                this.StateChanged?.Invoke();
            }
        }

        public void ResumeNavigation()
        {
            this.CanNavigate = true;
        }

        public void SuspendNavigation()
        {
            this.CanNavigate = false;
        }
    }
}