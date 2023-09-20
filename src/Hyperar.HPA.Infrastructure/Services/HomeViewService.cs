namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Domain.Interfaces;

    public class HomeViewService : IHomeViewService
    {
        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public HomeViewService(IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public List<Domain.SeniorTeam>? GetSeniorTeams()
        {
            return this.seniorTeamRepository.Query().ToList();
        }
    }
}