namespace Hyperar.HPA.UI.State.Interfaces
{
    using System;
    using UI.ViewModels;

    public interface INavigator
    {
        event Action? StateChanged;

        bool CanNavigate { get; }

        ViewModelBase? CurrentViewModel { get; set; }

        public uint? SelectedTeamId { get; set; }

        void ResumeNavigation();

        void SuspendNavigation();
    }
}