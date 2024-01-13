namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Application.Models.Players;
    using Application.Services;

    public class PlayersViewModel : ViewModelBase
    {
        private readonly IPlayersViewService playersViewService;

        private readonly uint selectedTeamId;

        private ObservableCollection<SeniorPlayer> selectedSeniorPlayers = new ObservableCollection<SeniorPlayer>();

        private int? selectedTabIndex;

        public PlayersViewModel(
            IPlayersViewService playersViewService,
            uint selectedTeamId)
        {
            this.playersViewService = playersViewService;
            this.selectedTeamId = selectedTeamId;
        }

        public Currency Currency { get; private set; } = new Currency();

        public ObservableCollection<SeniorPlayer> SelectedSeniorPlayers
        {
            get
            {
                return selectedSeniorPlayers;
            }
            set
            {
                selectedSeniorPlayers = value;
            }
        }

        public void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            var result = await this.playersViewService.GetSeniorPlayerAsync(this.selectedTeamId);

            this.SeniorPlayers = new ObservableCollection<SeniorPlayer>(result ?? Array.Empty<SeniorPlayer>());

            this.Currency = await this.playersViewService.GetManagerCurrencyAsync();

            this.OnPropertyChanged(nameof(this.SeniorPlayers));
        }
    }
}