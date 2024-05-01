namespace Hyperar.HPA.WinUI.State.Interface
{
    using System;
    using Hyperar.HPA.WinUI.ViewModels;

    public interface INavigator
    {
        event Action? StateChanged;

        bool CanNavigate { get; }

        ViewModelBase? CurrentPage { get; set; }

        void ResumeNavigation();

        void SuspendNavigation();
    }
}