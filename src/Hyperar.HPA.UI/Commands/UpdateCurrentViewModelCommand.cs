namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public class UpdateCurrentViewModelCommand : AsyncCommandBase, ICommand
    {
        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is ViewType viewType)
            {
                this.navigator.CurrentViewModel = await this.viewModelFactory.CreateAsyncViewModel(viewType);
            }
        }
    }
}