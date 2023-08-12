namespace Hyperar.HPA.UserInterface.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.UserInterface.Commands;
    using Hyperar.HPA.UserInterface.Enums;
    using Hyperar.HPA.UserInterface.Interfaces;
    using System.Windows.Input;

    public class MainViewModel : ViewModelBase
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

        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;

            this.navigator.StateChanged += Navigator_StateChanged;

            this.UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, this.viewModelFactory);
            this.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }

        private void Navigator_StateChanged()
        {
            this.OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            this.navigator.StateChanged -= Navigator_StateChanged;

            base.Dispose();
        }
    }
}
