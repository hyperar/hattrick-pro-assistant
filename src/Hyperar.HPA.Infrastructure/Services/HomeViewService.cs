namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Collections.Generic;
    using Hyperar.HPA.Application.Services;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class HomeViewService : IHomeViewService
    {
        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public HomeViewService(IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task<List<Domain.SeniorTeam>?> GetSeniorTeamsAsync()
        {
            return await this.seniorTeamRepository.Query().ToListAsync();
        }
    }
}