namespace Hyperar.HPA.WinUI.Commands
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using WinUI.Enums;
    using WinUI.State.Interface;
    using WinUI.ViewModels;
    using WinUI.ViewModels.Interface;

    internal class UpdateCurrentPageCommand : AsyncCommandBase, ICommand
    {
        private readonly MainViewModel MainViewModel;

        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public UpdateCurrentPageCommand(
            INavigator navigator,
            MainViewModel MainViewModel,
            IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.MainViewModel = MainViewModel;
            this.viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            if (parameter is ViewType viewType)
            {
                this.MainViewModel.CurrentPage = await this.viewModelFactory.CreateViewModelAsync(viewType);
            }

            this.navigator.ResumeNavigation();
        }
    }
}