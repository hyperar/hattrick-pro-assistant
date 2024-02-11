namespace Hyperar.HPA.WinUI.State
{
    using WinUI.State.Interface;

    public class TeamSelector : ITeamSelector
    {
        public uint SelectedTeamId { get; private set; }

        public void SetSelectedTeam(uint selectedTeamId)
        {
            this.SelectedTeamId = selectedTeamId;
        }
    }
}