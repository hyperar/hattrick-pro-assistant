namespace Hyperar.HPA.Application.Services
{
    using System.Threading.Tasks;
    using Shared.Models.UI.TeamSelection;

    public interface ITeamSelectionViewService
    {
        Task<Team[]> GetTeamsAsync();

        Task SetSelectedTeamAsync(long teamHattrickId);
    }
}