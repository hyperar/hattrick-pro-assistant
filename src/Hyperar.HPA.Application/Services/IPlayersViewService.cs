namespace Hyperar.HPA.Application.Services
{
    using System.Collections.Generic;
    using Application.Models.PlayersView;

    public interface IPlayersViewService
    {
        Task<SeniorPlayer[]> GetSeniorPlayerAsync(uint seniorTeamId);
    }
}