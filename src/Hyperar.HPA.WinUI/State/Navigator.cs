namespace Hyperar.HPA.WinUI.State
{
    using System;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Hyperar.HPA.WinUI.State.Interface;

    public class Navigator : ObservableObject, INavigator
    {
        private bool canNavigate;

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