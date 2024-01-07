namespace Hyperar.HPA.Application.Services
{
    using System.Threading.Tasks;
    using Application.Models.Home;

    public interface IHomeViewService
    {
        Task<TeamOverview> GetTeamsOverview(uint seniorTeamId);
    }
}