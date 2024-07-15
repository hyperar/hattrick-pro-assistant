namespace Hyperar.HPA.WinUI.State.Interface
{
    public interface ITeamSelector
    {
        long SelectedTeamHattrickId { get; }

        public void SetSelectedTeam(long selectedTeamHattrickId);
    }
}