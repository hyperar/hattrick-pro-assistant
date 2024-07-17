namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Services;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HPA.WinUI.Enums;
    using Shared.Models.UI.TeamSelection;
    using WinUI.State.Interface;

    public partial class TeamSelectionViewModel : AsyncViewModelBase
    {
        private readonly ITeamSelectionViewService teamSelectionViewService;

        [ObservableProperty]
        private ICollection<Team> teams;

        public TeamSelectionViewModel(
            INavigator navigator,
            ITeamSelectionViewService teamSelectionViewService) : base(navigator)
        {
            this.teamSelectionViewService = teamSelectionViewService;

            this.teams = new List<Team>();
        }

        public override async Task InitializeAsync()
        {
            this.Teams = new List<Team>(await this.teamSelectionViewService.GetTeamsAsync());

            if (this.Navigator.SelectedTeamHattrickId != 0)
            {
                this.Navigator.ResumeNavigation();
            }

            await base.InitializeAsync();
        }

        [RelayCommand]
        private async Task SelectTeamAsync(long selectedTeamHattrickId)
        {
            this.Navigator.SuspendNavigation();

            await this.teamSelectionViewService.SetSelectedTeamAsync(selectedTeamHattrickId);

            this.Navigator.SetSelectedTeam(selectedTeamHattrickId);

            this.Navigator.SetPageType(ViewType.Home);
        }
    }
}