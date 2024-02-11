namespace Hyperar.HPA.WinUI.ViewModels
{
    using Hyperar.HPA.WinUI.State.Interface;

    public class TeamSelectionViewModel : AsyncViewModelBase
    {
        private readonly ITeamSelector teamSelector;

        public TeamSelectionViewModel(
            INavigator navigator,
            ITeamSelector teamSelector) : base(navigator)
        {
            this.teamSelector = teamSelector;
        }
    }
}