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

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        private readonly IRepository<Domain.User> userRepository;

        public TeamSelectionViewService(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository,
            IRepository<Domain.User> userRepository)
        {
            this.databaseContext = databaseContext;
            this.leagueRepository = leagueRepository;
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<Team[]> GetTeamsAsync()
        {
            var user = await this.userRepository.Query().SingleOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user, nameof(user));

            if (user.Manager == null)
            {
                return Array.Empty<Team>();
            }

            long lastSelectedTeamId = user.SelectedTeamHattrickId ?? 0;

            var teamList = new List<Team>();

            foreach (var team in user.Manager.SeniorTeams)
            {
                var league = await this.leagueRepository.GetByHattrickIdAsync(team.LeagueHattrickId);

                ArgumentNullException.ThrowIfNull(league, nameof(league));

                var series = team.Series.Single(x => x.Season == team.League.Season);

                teamList.Add(
                    new Team
                    {
                        HattrickId = team.HattrickId,
                        Name = team.Name,
                        LogoBytes = team.LogoBytes,
                        HomeMatchKitBytes = team.HomeMatchKitBytes,
                        AwayMatchKitBytes = team.AwayMatchKitBytes,
                        IsSelected = team.HattrickId == lastSelectedTeamId,
                        League = new League
                        {
                            HattrickId = team.League.HattrickId,
                            Name = team.League.Name,
                            FlagBytes = team.League.FlagBytes
                        },
                        Country = new Country
                        {
                            HattrickId = team.Region.Country.HattrickId,
                            Name = team.Region.Country.Name,
                            FlagBytes = team.Region.Country.League.FlagBytes
                        },
                        Region = new Region
                        {
                            HattrickId = team.Region.HattrickId,
                            Name = team.Region.Name
                        },
                        Series = new Series
                        {
                            HattrickId = series.SeriesHattrickId,
                            Name = team.Name
                        }
                    });
            }

            return teamList.ToArray();
        }

        public async Task SetSelectedTeamAsync(long teamHattrickId)
        {
            var user = await this.userRepository.Query().SingleOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user, nameof(user));

            user.SelectedTeamHattrickId = teamHattrickId;

            this.userRepository.Update(user);

            await this.databaseContext.SaveAsync();
        }
    }
}