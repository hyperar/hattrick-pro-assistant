namespace Hyperar.HPA.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Hyperar.HPA.Application.Services;

    public class HomeViewModel : ViewModelBase
    {
        private readonly IHomeViewService homeViewService;

        private int? selectedTabIndex;

        private ObservableCollection<Domain.SeniorTeam>? seniorTeams;

        public HomeViewModel(IHomeViewService homeViewService)
        {
            this.homeViewService = homeViewService;
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
                this.OnPropertyChanged(nameof(this.SeniorTeams));
                this.OnPropertyChanged(nameof(this.SelectedTabIndex));
            }
        }

        public ObservableCollection<Domain.SeniorTeam> SeniorTeams
        {
            get
            {
                return this.seniorTeams != null
                     ? new ObservableCollection<Domain.SeniorTeam>(this.seniorTeams)
                     : new ObservableCollection<Domain.SeniorTeam>();
            }
        }

        public override async Task InitializeAsync()
        {
            var result = await this.homeViewService.GetSeniorTeamsAsync();

            this.seniorTeams = new ObservableCollection<Domain.SeniorTeam>(result ?? new List<Domain.SeniorTeam>());

            this.OnPropertyChanged(nameof(this.SeniorTeams));
        }
    }
}