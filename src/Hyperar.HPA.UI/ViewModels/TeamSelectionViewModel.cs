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

        public ObservableCollection<SeniorTeam> SeniorTeams { get; set; } = new ObservableCollection<SeniorTeam>();

        public override async Task InitializeAsync()
        {
            this.SeniorTeams = new ObservableCollection<SeniorTeam>(await this.teamSelectionViewService.GetSeniorTeams());

            this.OnPropertyChanged(nameof(this.SeniorTeams));
        }
    }
}