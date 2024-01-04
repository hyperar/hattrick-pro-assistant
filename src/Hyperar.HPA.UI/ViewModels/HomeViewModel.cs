namespace Hyperar.HPA.UI.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.Models;
    using Hyperar.HPA.Application.Services;

    public class HomeViewModel : ViewModelBase
    {
        private readonly IHomeViewService homeViewService;

        private readonly uint selectedTeamId;

        private int? selectedTabIndex;

        public HomeViewModel(IHomeViewService homeViewService, uint selectedTeamId)
        {
            this.homeViewService = homeViewService;
            this.selectedTeamId = selectedTeamId;
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

        public ObservableCollection<SeniorPlayer>? SeniorPlayers { get; private set; }

        public override async Task InitializeAsync()
        {
            var result = await this.homeViewService.GetSeniorPlayerAsync(this.selectedTeamId);

            this.SeniorPlayers = new ObservableCollection<SeniorPlayer>(result ?? Array.Empty<SeniorPlayer>());

            this.OnPropertyChanged(nameof(this.SeniorPlayers));
        }
    }
}