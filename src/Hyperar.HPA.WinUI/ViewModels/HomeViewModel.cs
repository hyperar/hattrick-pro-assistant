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

        [ObservableProperty]
        private Team? team;

        public HomeViewModel(
            INavigator navigator,
            IHomeViewService homeViewService) : base(navigator)
        {
            this.homeViewService = homeViewService;
        }

        public Currency? Currency { get; set; }

        public override async Task InitializeAsync()
        {
            ArgumentNullException.ThrowIfNull(this.Navigator.SelectedTeamHattrickId, nameof(this.Navigator.SelectedTeamHattrickId));

            this.Currency = await this.homeViewService.GetManagerCurrencyAsync();

            this.Team = await this.homeViewService.GetTeamsOverviewAsync(
                this.Navigator.SelectedTeamHattrickId);

            await base.InitializeAsync();

            this.Navigator.ResumeNavigation();
        }
    }
}