namespace Hyperar.HPA.Application.Services
{
    using System.Collections.Generic;

    public interface IHomeViewService
    {
        List<Domain.SeniorTeam>? GetSeniorTeams();
    }
}