namespace Hyperar.HPA.WinUI.State.Interface
{
    public interface ITeamSelector
    {
        long SelectedTeamId { get; }

        public void SetSelectedTeam(long selectedTeam);
    }
}