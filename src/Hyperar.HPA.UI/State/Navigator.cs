namespace Hyperar.HPA.UI.State
{
    using System;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels;

    public class Navigator : INavigator
    {
        private bool canNavigate;

        private ViewModelBase? currentViewModel;

        private uint? selectedTeamId;

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

        public uint? SelectedTeamId
        {
            get
            {
                return this.selectedTeamId;
            }
            set
            {
                this.selectedTeamId = value;

                this.StateChanged?.Invoke();
            }
        }
    }
}