namespace Hyperar.HPA.WinUI.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Application.Services;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Shared.Models.UI.Home;
    using WinUI.State.Interface;

    public partial class HomeViewModel : AsyncViewModelBase
    {
        private readonly IHomeViewService homeViewService;

        private readonly ITeamSelector teamSelector;

        [ObservableProperty]
        private Team? team;

        public HomeViewModel(
            INavigator navigator,
            ITeamSelector teamSelector,
            IHomeViewService homeViewService) : base(navigator)
        {
            this.homeViewService = homeViewService;
            this.teamSelector = teamSelector;
        }

        public Currency Currency { get; set; }

        public override async Task InitializeAsync()
        {
            ArgumentNullException.ThrowIfNull(this.teamSelector.SelectedTeamId, nameof(this.teamSelector.SelectedTeamId));

            this.Currency = await this.homeViewService.GetManagerCurrencyAsync();

            this.Team = await this.homeViewService.GetTeamsOverviewAsync(
                this.teamSelector.SelectedTeamId);

            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }
    }
}