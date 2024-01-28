namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Application.Models.TeamSelection;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TeamSelectionViewService : ITeamSelectionViewService
    {
        private readonly IHattrickRepository<Domain.Team> teamRepository;

        public TeamSelectionViewService(IHattrickRepository<Domain.Team> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public async Task<Team[]> GetTeams()
        {
            return await this.teamRepository.Query()
                                            .OrderByDescending(x => x.IsPrimary)
                                            .OrderBy(x => x.Name)
                                            .Select(x => new Team
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
                                                Series = new Series
                                                {
                                                    HattrickId = x.SeriesHattrickId,
                                                    Name = x.SeriesName
                                                }
                                            }).ToArrayAsync();
        }
    }
}