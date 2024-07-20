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

        private long selectedTeamHattrickId;

        public event Action? CanNavigateChanged;

        public event Action? CurrentPageChanged;

        public event Action? PageTypeChanged;

        public event Action? SelectedTeamChanged;

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

            private set
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

            private set
            {
                this.pageType = value;

                this.PageTypeChanged?.Invoke();
            }
        }

        public long SelectedTeamHattrickId
        {
            get
            {
                return this.selectedTeamHattrickId;
            }

            private set
            {
                this.selectedTeamHattrickId = value;

                this.SelectedTeamChanged?.Invoke();
            }
        }

        public void RebuildMenu()
        {
            this.SelectedTeamChanged?.Invoke();
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

        public void SetSelectedTeam(long selectedTeamHattrickId)
        {
            this.SelectedTeamHattrickId = selectedTeamHattrickId;
        }

        public void SuspendNavigation()
        {
            this.CanNavigate = false;
        }
    }
}