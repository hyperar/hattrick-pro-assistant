namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Hyperar.HPA.Application.Services;
    using Shared.Models.UI.TeamSelection;
    using WinUI.State.Interface;

    public partial class TeamSelectionViewModel : AsyncViewModelBase
    {
        private readonly ITeamSelector teamSelector;

        private readonly ITeamSelectionViewService teamSelectionViewService;

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
        }
    }
}