namespace Hyperar.HPA.WinUI.State.Interface
{
    using System;
    using Hyperar.HPA.WinUI.Enums;
    using WinUI.ViewModels;

    public interface INavigator
    {
        event Action? CanNavigateChanged;

        event Action? CurrentPageChanged;

        event Action? PageTypeChanged;

        event Action? SelectedTeamChanged;

        bool CanNavigate { get; }

        ViewModelBase? CurrentPage { get; }

        ViewType PageType { get; }

        long SelectedTeamHattrickId { get; }

        public void RebuildMenu();

        void ResumeNavigation();

        void SetCurrentPage(ViewModelBase? currentPage);

        void SetPageType(ViewType viewType);

        public void SetSelectedTeam(long selectedTeamHattrickId);

        void SuspendNavigation();
    }
}