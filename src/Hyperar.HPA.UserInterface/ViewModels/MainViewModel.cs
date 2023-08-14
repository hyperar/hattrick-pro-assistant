namespace Hyperar.HPA.UserInterface.ViewModels
{
    using System.Windows.Input;
    using Hyperar.HPA.UserInterface.Commands;
    using Hyperar.HPA.UserInterface.Enums;
    using Hyperar.HPA.UserInterface.State.Interfaces;
    using Hyperar.HPA.UserInterface.ViewModels.Interfaces;

    public class MainViewModel : AuthorizedViewModelBase
    {
        private readonly IViewModelFactory viewModelFactory;

        private readonly INavigator navigator;

        public ViewModelBase? CurrentViewModel
        {
            get
            {
                return this.navigator.CurrentViewModel;
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IAuthorizer authorizer) : base(authorizer)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;

            this.navigator.StateChanged += this.Navigator_StateChanged;

            this.UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, this.viewModelFactory);

            this.Initialize();

            if (!this.IsAuthorized)
            {
                this.UpdateCurrentViewModelCommand.Execute(ViewType.Permissions);
            }
            else
            {
                this.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
            }
        }

        private void Navigator_StateChanged()
        {
            this.OnPropertyChanged(nameof(this.CurrentViewModel));
        }

        public override void Dispose()
        {
            this.navigator.StateChanged -= this.Navigator_StateChanged;

            base.Dispose();
        }
    }
}
