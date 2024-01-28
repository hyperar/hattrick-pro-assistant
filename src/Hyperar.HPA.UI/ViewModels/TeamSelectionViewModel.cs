namespace Hyperar.HPA.UI.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Application.Models.TeamSelection;
    using Application.Services;
    using UI.Commands;
    using UI.State.Interfaces;
    using UI.ViewModels.Interfaces;

    public class TeamSelectionViewModel : ViewModelBase
    {
        private readonly ITeamSelectionViewService teamSelectionViewService;

        public TeamSelectionViewModel(
            ITeamSelectionViewService teamSelectionViewService,
            INavigator navigator,
            IViewModelFactory viewModelFactory)
        {
            this.teamSelectionViewService = teamSelectionViewService;
            this.ChangeSelectedTeamCommand = new ChangeSelectedTeamCommand(navigator, viewModelFactory);

            this.SelectedTeamId = navigator.SelectedTeamId ?? 0;
        }

        public ICommand ChangeSelectedTeamCommand { get; }

        public uint SelectedTeamId { get; private set; }

        public ObservableCollection<Team> Teams { get; set; } = new ObservableCollection<Team>();

        public override async Task InitializeAsync()
        {
            this.Teams = new ObservableCollection<Team>(await this.teamSelectionViewService.GetTeams());

            this.OnPropertyChanged(nameof(this.Teams));
        }
    }
}