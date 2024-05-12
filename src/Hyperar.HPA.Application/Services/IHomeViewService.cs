namespace Hyperar.HPA.Application.Services
{
    using System.Threading.Tasks;
    using Shared.Models.UI.Home;

    public interface IHomeViewService
    {
        Task<Team> GetTeamsOverviewAsync(long teamId);
    }
}