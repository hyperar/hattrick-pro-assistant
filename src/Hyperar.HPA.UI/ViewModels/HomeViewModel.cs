namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Application.Models.Home;
    using Application.Services;
    using UI.State.Interfaces;

    public class HomeViewModel : ViewModelBase
    {
        private readonly IHomeViewService homeViewService;

        private readonly INavigator navigator;

        public HomeViewModel(IHomeViewService homeViewService, INavigator navigator)
        {
            this.homeViewService = homeViewService;
            this.navigator = navigator;
        }

        public TeamOverview TeamOverview { get; set; } = new TeamOverview();

        public override async Task InitializeAsync()
        {
            ArgumentNullException.ThrowIfNull(this.navigator.SelectedTeamId, nameof(this.navigator.SelectedTeamId));

            this.TeamOverview = await this.homeViewService.GetTeamsOverview(this.navigator.SelectedTeamId.Value);

            this.OnPropertyChanged(nameof(this.TeamOverview));
        }
    }
}