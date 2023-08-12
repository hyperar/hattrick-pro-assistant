namespace Hyperar.HPA.UserInterface.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Hyperar.HPA.UserInterface.Enums;
    using Hyperar.HPA.UserInterface.Interfaces;

    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly INavigator navigator;
        private readonly IViewModelFactory viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

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
