namespace Hyperar.HPA.WinUI.State
{
    using WinUI.State.Interface;

    public class TeamSelector : ITeamSelector
    {
        public long SelectedTeamId { get; private set; }

        public void SetSelectedTeam(long selectedTeamId)
        {
            this.SelectedTeamId = selectedTeamId;
        }
    }
}