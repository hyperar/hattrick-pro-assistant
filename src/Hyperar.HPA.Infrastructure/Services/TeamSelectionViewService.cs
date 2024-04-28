namespace Hyperar.HPA.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Models.UI.TeamSelection;

    public class TeamSelectionViewService : ITeamSelectionViewService
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        private readonly IRepository<Domain.User> userRepository;

        public TeamSelectionViewService(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.Team> teamRepository,
            IRepository<Domain.User> userRepository)
        {
            this.databaseContext = databaseContext;
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<Team[]> GetTeamsAsync()
        {
            var user = await this.userRepository.Query().SingleOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user, nameof(user));

            long lastSelectedTeamId = user.LastSelectedTeamHattrickId ?? 0;

            return await this.teamRepository.Query()
                                            .OrderByDescending(x => x.IsPrimary)
                                            .OrderBy(x => x.Name)
                                            .Select(x => new Team
                                            {
                                                HattrickId = x.HattrickId,
                                                Name = x.Name,
                                                LogoBytes = x.LogoBytes,
                                                HomeMatchKitBytes = x.HomeMatchKitBytes,
                                                AwayMatchKitBytes = x.AwayMatchKitBytes,
                                                IsSelected = x.HattrickId == lastSelectedTeamId,
                                                League = new League
                                                {
                                                    HattrickId = x.League.HattrickId,
                                                    Name = x.League.Name,
                                                    FlagBytes = x.League.FlagBytes
                                                },
                                                Country = new Country
                                                {
                                                    HattrickId = x.Region.Country.HattrickId,
                                                    Name = x.Region.Country.Name,
                                                    FlagBytes = x.Region.Country.League.FlagBytes
                                                },
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