namespace Hyperar.HPA.WinUI.State.Interface
{
    public interface ITeamSelector
    {
        uint SelectedTeamId { get; }

        public void SetSelectedTeam(uint selectedTeam);
    }
}