namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.Windows.Input;
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType viewType)
            {
                this.navigator.CurrentViewModel = this.viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}