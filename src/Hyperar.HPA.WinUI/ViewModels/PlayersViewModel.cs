namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.Threading.Tasks;
    using WinUI.State.Interface;
    using Shared.Models.UI.Players;
    using CommunityToolkit.Mvvm.ComponentModel;
    using System;
    using Application.Services;
    using System.Collections.Generic;
    using System.Linq;

    public partial class PlayersViewModel : AsyncViewModelBase
    {
        private readonly IPlayersViewService playersViewService;
        private readonly ITeamSelector teamSelector;
        public PlayersViewModel(
            INavigator navigator,
            ITeamSelector teamSelector,
            IPlayersViewService playersViewService) : base(navigator)
        {
            this.Currency = new Currency();
            this.players = new List<Player>();
            this.playersViewService = playersViewService;
            this.teamSelector = teamSelector;
        }

        public override async Task InitializeAsync()
        {
            this.Currency = await this.playersViewService.GetManagerCurrencyAsync();
            this.Players = (await this.playersViewService.GetPlayersAsync(this.teamSelector.SelectedTeamId)).ToList();

            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }

        public Currency Currency { get; set; }

        [ObservableProperty]
        private List<Player> players;
    }
}