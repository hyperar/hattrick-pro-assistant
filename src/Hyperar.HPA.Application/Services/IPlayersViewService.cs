namespace Hyperar.HPA.Application.Services
{
    using System.Collections.Generic;

    public interface IPlayersViewService
    {
        Task<Models.SeniorPlayer[]> GetSeniorPlayerAsync(uint seniorTeamId);
    }
}