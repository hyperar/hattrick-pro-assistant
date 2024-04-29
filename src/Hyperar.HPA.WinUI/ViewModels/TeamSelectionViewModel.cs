namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HPA.Application.Services;
    using Shared.Models.UI.TeamSelection;
    using WinUI.State.Interface;

    public partial class TeamSelectionViewModel : AsyncViewModelBase
    {
        private readonly ITeamSelectionViewService teamSelectionViewService;

        private readonly ITeamSelector teamSelector;

        [ObservableProperty]
        private ICollection<Team> teams;

        public TeamSelectionViewModel(
            INavigator navigator,
            ITeamSelector teamSelector,
            ITeamSelectionViewService teamSelectionViewService) : base(navigator)
        {
            this.teamSelector = teamSelector;
            this.teamSelectionViewService = teamSelectionViewService;

            this.teams = new List<Team>();
        }

        public override async Task InitializeAsync()
        {
            this.Teams = new List<Team>(await this.teamSelectionViewService.GetTeamsAsync());

            await base.InitializeAsync();

            if (this.teamSelector.SelectedTeamId == 0)
            {
                this.Navigator.SuspendNavigation();
            }
        }

        [RelayCommand]
        private async Task SelectTeamAsync(long selectedTeamId)
        {
            this.Navigator.SuspendNavigation();

            this.teamSelector.SetSelectedTeam(selectedTeamId);

            await this.teamSelectionViewService.SetSelectedTeamAsync(selectedTeamId);

            await this.InitializeAsync();

            this.Navigator.ResumeNavigation();
        }
    }
}