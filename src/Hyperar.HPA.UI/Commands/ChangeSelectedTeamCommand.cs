namespace Hyperar.HPA.UI.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using UI.Enums;
    using UI.State.Interfaces;
    using UI.ViewModels.Interfaces;

    public class ChangeSelectedTeamCommand : AsyncCommandBase, ICommand
    {
        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public ChangeSelectedTeamCommand(
            INavigator navigator,
            IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is uint selectedTeamId)
            {
                this.navigator.SelectedTeamId = selectedTeamId;

                this.navigator.CurrentViewModel = await this.viewModelFactory.CreateAsyncViewModel(ViewType.Home);
            }
        }
    }
}