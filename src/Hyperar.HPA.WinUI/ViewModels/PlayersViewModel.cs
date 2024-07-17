namespace Hyperar.HPA.WinUI.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Services;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Shared.Models.UI.Players;
    using WinUI.State.Interface;

    public partial class PlayersViewModel : AsyncViewModelBase
    {
        private readonly IPlayersViewService playersViewService;

        [ObservableProperty]
        private List<Player> players;

        [ObservableProperty]
        private Player? selectedPlayer;

        public PlayersViewModel(
            INavigator navigator,
            IPlayersViewService playersViewService) : base(navigator)
        {
            this.Currency = new Currency();
            this.players = new List<Player>();
            this.playersViewService = playersViewService;
        }

        public Currency Currency { get; set; }

        public override async Task InitializeAsync()
        {
            this.Currency = await this.playersViewService.GetManagerCurrencyAsync();
            this.Players = (await this.playersViewService.GetPlayersAsync(this.Navigator.SelectedTeamHattrickId)).ToList();

            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }
    }
}