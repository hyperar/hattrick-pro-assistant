namespace Hyperar.HPA.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Application.Models.HomeView;

    public interface IHomeViewService
    {
        Task<TeamOverview> GetTeamsOverview(uint seniorTeamId);
    }
}
