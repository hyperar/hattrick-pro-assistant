namespace Hyperar.HPA.UI.Commands
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using UI.Enums;
    using UI.State.Interfaces;
    using UI.ViewModels.Interfaces;

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