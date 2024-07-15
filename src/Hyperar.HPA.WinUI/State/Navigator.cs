namespace Hyperar.HPA.WinUI.State
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Hyperar.HPA.WinUI.Enums;
    using WinUI.State.Interface;
    using WinUI.ViewModels;

    public class Navigator : ObservableObject, INavigator
    {
        private bool canNavigate;

        private ViewModelBase? currentPage;

        private ViewType pageType;

        public event Action? CanNavigateChanged;

        public event Action? CurrentPageChanged;

        public event Action? PageTypeChanged;

        public bool CanNavigate
        {
            get
            {
                return this.canNavigate;
            }

            private set
            {
                this.canNavigate = value;

                this.CanNavigateChanged?.Invoke();
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

                this.CurrentPageChanged?.Invoke();
            }
        }

        public ViewType PageType
        {
            get
            {
                return this.pageType;
            }

            set
            {
                this.pageType = value;

                this.PageTypeChanged?.Invoke();
            }
        }

        public void ResumeNavigation()
        {
            this.CanNavigate = true;
        }

        public void SetCurrentPage(ViewModelBase? currentPage)
        {
            this.CurrentPage = currentPage;
        }

        public void SetPageType(ViewType viewType)
        {
            this.PageType = viewType;
        }

        public void SuspendNavigation()
        {
            this.CanNavigate = false;
        }
    }
}