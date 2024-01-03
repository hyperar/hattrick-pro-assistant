namespace Hyperar.HPA.Application.Services
{
    using System.Collections.Generic;

    public interface IHomeViewService
    {
        Task<Models.SeniorPlayer[]> GetSeniorPlayerAsync(uint seniorTeamId);
    }
}