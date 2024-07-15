namespace Hyperar.HPA.WinUI.State
{
    using WinUI.State.Interface;

    public class TeamSelector : ITeamSelector
    {
        public long SelectedTeamHattrickId { get; private set; }

        public void SetSelectedTeam(long selectedTeamHattrickId)
        {
            this.SelectedTeamHattrickId = selectedTeamHattrickId;
        }
    }
}