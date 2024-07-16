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

        bool CanNavigate { get; }

        ViewModelBase? CurrentPage { get; }

        ViewType PageType { get; }

        void ResumeNavigation();

        void SetCurrentPage(ViewModelBase? currentPage);

        void SetPageType(ViewType viewType);

        void SuspendNavigation();
    }
}