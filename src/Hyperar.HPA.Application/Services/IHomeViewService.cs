namespace Hyperar.HPA.Application.Services
{
    using System.Collections.Generic;

    public interface IHomeViewService
    {
        Task<List<Domain.SeniorTeam>?> GetSeniorTeamsAsync();
    }
}