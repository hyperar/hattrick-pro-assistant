namespace Hyperar.HPA.UI.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Hyperar.HPA.UI.Commands;
    using Hyperar.HPA.UI.Enums;
    using Hyperar.HPA.UI.State.Interfaces;
    using Hyperar.HPA.UI.ViewModels.Interfaces;

    public class MainViewModel : AuthorizedViewModelBase
    {
        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public MainViewModel(
            INavigator navigator,
            IViewModelFactory viewModelFactory,
            IAuthorizer authorizer) : base(authorizer)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;

            this.navigator.StateChanged += this.Navigator_StateChanged;

            this.UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, this.viewModelFactory);
        }

        public bool CanNavigate
        {
            get
            {
                return this.navigator.CanNavigate;
            }
        }

        public ViewModelBase? CurrentViewModel
        {
            get
            {
                return this.navigator.CurrentViewModel;
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; }

        public override void Dispose()
        {
            this.navigator.StateChanged -= this.Navigator_StateChanged;

            base.Dispose();
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (this.IsAuthorized.HasValue && this.IsAuthorized.Value)
            {
                this.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
            }
            else
            {
                this.UpdateCurrentViewModelCommand.Execute(ViewType.Permissions);
            }
        }

        private void Navigator_StateChanged()
        {
            this.OnPropertyChanged(nameof(this.CanNavigate));
            this.OnPropertyChanged(nameof(this.CurrentViewModel));
        }
    }
}