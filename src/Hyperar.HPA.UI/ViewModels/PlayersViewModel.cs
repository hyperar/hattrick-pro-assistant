namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Application.Models.Players;
    using Application.Services;

    public class PlayersViewModel : ViewModelBase
    {
        private readonly IPlayersViewService playersViewService;

        private readonly uint selectedTeamId;

        private Player? selectedComparisonPlayer;

        private Player? selectedPlayer;

        private int? selectedTabIndex;

        public PlayersViewModel(
            IPlayersViewService playersViewService,
            uint selectedTeamId)
        {
            this.playersViewService = playersViewService;
            this.selectedTeamId = selectedTeamId;
        }

        public Currency Currency { get; private set; } = new Currency();

        public ObservableCollection<Player> Players { get; private set; } = new ObservableCollection<Player>();

        public Player? SelectedComparisonPlayer
        {
            get
            {
                return this.selectedComparisonPlayer;
            }
            set
            {
                this.selectedComparisonPlayer = value;
                OnPropertyChanged(nameof(this.SelectedComparisonPlayer));
            }
        }

        public Player? SelectedPlayer
        {
            get
            {
                return this.selectedPlayer;
            }
            set
            {
                this.selectedPlayer = value;
                OnPropertyChanged(nameof(this.SelectedPlayer));
            }
        }

        public int SelectedTabIndex
        {
            get
            {
                return this.selectedTabIndex ?? 0;
            }
            set
            {
                this.selectedTabIndex = value;
                this.OnPropertyChanged(nameof(this.Players));
                this.OnPropertyChanged(nameof(this.SelectedTabIndex));
            }
        }

        public override async Task InitializeAsync()
        {
            this.Currency = await this.playersViewService.GetManagerCurrencyAsync();

            var result = await this.playersViewService.GetPlayerAsync(this.selectedTeamId);

            this.Players = new ObservableCollection<Player>(result ?? Array.Empty<Player>());

            this.OnPropertyChanged(nameof(this.Players));
        }
    }
}