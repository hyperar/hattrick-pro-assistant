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
        private readonly MainWindowViewModel mainWindowViewModel;

        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public UpdateCurrentPageCommand(
            INavigator navigator,
            MainWindowViewModel mainWindowViewModel,
            IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.mainWindowViewModel = mainWindowViewModel;
            this.viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            if (parameter is ViewType viewType)
            {
                this.mainWindowViewModel.CurrentPage = await this.viewModelFactory.CreateViewModel(viewType);
            }

            this.navigator.ResumeNavigation();
        }
    }
}