namespace Hyperar.HPA.WinUI.State.Interface
{
    using System;

    public interface INavigator
    {
        event Action? StateChanged;

        bool CanNavigate { get; }

        void ResumeNavigation();

        void SuspendNavigation();
    }
}