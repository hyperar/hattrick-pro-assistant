namespace Hyperar.HPA.Application.Interfaces
{
    using System.Collections.Generic;

    public interface IHomeViewService
    {
        List<Domain.SeniorTeam>? GetSeniorTeams();
    }
}