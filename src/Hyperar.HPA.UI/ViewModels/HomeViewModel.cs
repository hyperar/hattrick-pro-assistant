namespace Hyperar.HPA.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Hyperar.HPA.Application.Services;

    public class HomeViewModel : ViewModelBase
    {
        private readonly IHomeViewService homeViewService;

        private int? selectedTabIndex;

        private List<Domain.SeniorTeam>? seniorTeams;

        public HomeViewModel(IHomeViewService homeViewService)
        {
            this.homeViewService = homeViewService;

            this.Initialize();
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

        private void Initialize()
        {
            this.seniorTeams = this.homeViewService.GetSeniorTeams();

            this.OnPropertyChanged(nameof(this.SeniorTeams));
        }
    }
}