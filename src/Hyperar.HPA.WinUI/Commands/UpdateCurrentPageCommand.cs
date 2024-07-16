namespace Hyperar.HPA.WinUI.Commands
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using WinUI.Enums;
    using WinUI.State.Interface;
    using WinUI.ViewModels.Interface;

    internal class UpdateCurrentPageCommand : AsyncCommandBase, ICommand
    {
        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public UpdateCurrentPageCommand(
            INavigator navigator,
            IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            if (parameter is ViewType viewType)
            {
                this.navigator.SetCurrentPage(
                    await this.viewModelFactory.CreateViewModelAsync(
                        viewType));
            }
        }
    }
}