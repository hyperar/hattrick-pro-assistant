namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Models.Players;
    using Application.Services;

    public class PlayersViewModel : ViewModelBase
    {
        private readonly IPlayersViewService playersViewService;

        private readonly uint selectedTeamId;

        private SeniorPlayer? selectedSeniorPlayer;

        private int? selectedTabIndex;

        public PlayersViewModel(
            IPlayersViewService playersViewService,
            uint selectedTeamId)
        {
            this.playersViewService = playersViewService;
            this.selectedTeamId = selectedTeamId;
        }

        public Currency Currency { get; private set; } = new Currency();

        public SeniorPlayer? SelectedSeniorPlayer
        {
            get
            {
                return this.selectedSeniorPlayer;
            }
            set
            {
                this.selectedSeniorPlayer = value;
                OnPropertyChanged(nameof(this.SelectedSeniorPlayer));
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
                this.OnPropertyChanged(nameof(this.SeniorPlayers));
                this.OnPropertyChanged(nameof(this.SelectedTabIndex));
            }
        }

        public ObservableCollection<SeniorPlayer> SeniorPlayers { get; private set; } = new ObservableCollection<SeniorPlayer>();

        public override async Task InitializeAsync()
        {
            this.Currency = await this.playersViewService.GetManagerCurrencyAsync();

            var result = await this.playersViewService.GetSeniorPlayerAsync(this.selectedTeamId);

            this.SeniorPlayers = new ObservableCollection<SeniorPlayer>(result ?? Array.Empty<SeniorPlayer>());
            this.SelectedSeniorPlayer = this.SeniorPlayers.FirstOrDefault();

            this.OnPropertyChanged(nameof(this.SeniorPlayers));
        }
    }
}