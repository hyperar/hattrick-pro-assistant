namespace Hyperar.HPA.Application.Services
{
    using Application.Models.Players;

    public interface IPlayersViewService
    {
        Task<SeniorPlayer[]> GetSeniorPlayerAsync(uint seniorTeamId);
    }
}