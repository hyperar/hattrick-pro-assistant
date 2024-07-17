namespace Hyperar.HPA.WinUI.State.Interface
{
    using System;
    using Hyperar.HPA.WinUI.Enums;
    using WinUI.ViewModels;

    public interface INavigator
    {
        event Action? CanNavigateChanged;

        event Action? SelectedTeamChanged;

        event Action? CurrentPageChanged;

        event Action? PageTypeChanged;

        bool CanNavigate { get; }

        ViewModelBase? CurrentPage { get; }

        ViewType PageType { get; }

        long SelectedTeamHattrickId { get; }

        void ResumeNavigation();

        void SetCurrentPage(ViewModelBase? currentPage);

        void SetPageType(ViewType viewType);

        public void SetSelectedTeam(long selectedTeamHattrickId);

        public void RebuildMenu();

        void SuspendNavigation();
    }
}