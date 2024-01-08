namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Application.Models.TeamSelection;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TeamSelectionViewService : ITeamSelectionViewService
    {
        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public TeamSelectionViewService(IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task<SeniorTeam[]> GetSeniorTeams()
        {
            return await this.seniorTeamRepository.Query()
                                                  .OrderByDescending(x => x.IsPrimary)
                                                  .OrderBy(x => x.Name)
                                                  .Select(x => new SeniorTeam
                                                  {
                                                      HattrickId = x.HattrickId,
                                                      Name = x.Name,
                                                      Country = new Country
                                                      {
                                                          HattrickId = x.Region.Country.HattrickId,
                                                          Name = x.Region.Country.Name
                                                      },
                                                      Logo = x.Logo,
                                                      Region = new Region
                                                      {
                                                          HattrickId = x.Region.HattrickId,
                                                          Name = x.Region.Name
                                                      },
                                                      SeniorSeries = new SeniorSeries
                                                      {
                                                          HattrickId = x.SeniorSeriesHattrickId,
                                                          Name = x.SeniorSeriesName
                                                      }
                                                  }).ToArrayAsync();
        }
    }
}