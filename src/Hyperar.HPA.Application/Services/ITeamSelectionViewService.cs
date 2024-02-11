namespace Hyperar.HPA.Application.Services
{
    using System.Threading.Tasks;
    using Application.Models.TeamSelection;

    public interface ITeamSelectionViewService
    {
        Task<Team[]> GetTeams();
    }
}