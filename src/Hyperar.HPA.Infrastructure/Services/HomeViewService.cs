namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Models.Home;
    using Application.Services;
    using Common.Enums;
    using Domain.Interfaces;

    public class HomeViewService : IHomeViewService
    {
        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public HomeViewService(IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public async Task<TeamOverview> GetTeamsOverview(uint teamId)
        {
            Domain.Senior.Team? team = await this.teamRepository.GetByHattrickIdAsync(teamId);

            ArgumentNullException.ThrowIfNull(team, nameof(team));

            return new TeamOverview
            {
                Manager = new Manager
                {
                    HattrickId = team.Manager.HattrickId,
                    UserName = team.Manager.UserName,
                    SupporterTier = team.Manager.SupporterTier,
                    Avatar = team.Manager.AvatarBytes,
                    Country = new Country
                    {
                        HattrickId = team.Manager.Country.HattrickId,
                        Name = team.Manager.Country.Name,
                    },
                },
                Team = new Team
                {
                    HattrickId = team.HattrickId,
                    Name = team.Name,
                    Logo = team.LogoBytes,
                    Country = new Country
                    {
                        HattrickId = team.Region.Country.HattrickId,
                        Name = team.Region.Country.Name,
                    },
                    PlayedMatches = team.OverviewMatches.Where(y => y.Status == MatchStatus.Finished)
                                                        .OrderBy(y => y.StartsOn)
                                                        .Select(y => new PlayedMatch
                                                        {
                                                            Date = y.StartsOn,
                                                            HomeTeam = new Team
                                                            {
                                                                HattrickId = y.HomeTeamHattrickId,
                                                                Name = y.HomeTeamName
                                                            },
                                                            HomeGoals = y.HomeGoals ?? 0,
                                                            AwayTeam = new Team
                                                            {
                                                                HattrickId = y.AwayTeamHattrickId,
                                                                Name = y.AwayTeamName
                                                            },
                                                            AwayGoals = y.AwayGoals ?? 0,
                                                            Type = y.Type
                                                        }).ToArray(),
                    Region = new Region
                    {
                        HattrickId = team.Region.HattrickId,
                        Name = team.Region.Name
                    },
                    Players = team.Players.OrderBy(x => x.ShirtNumber)
                                          .ThenBy(x => x.LastName)
                                          .ThenBy(x => x.FirstName)
                                          .ThenBy(x => x.HattrickId)
                                          .Select(x => new Player
                                          {
                                              HattrickId = x.HattrickId,
                                              FirstName = x.FirstName,
                                              NickName = x.NickName,
                                              LastName = x.LastName,
                                              BookingStatus = x.BookingStatus,
                                              HasMotherClubBonus = x.HasMotherClubBonus,
                                              IsTransferListed = x.IsTransferListed,
                                              HealthStatus = x.Health,
                                              Specialty = x.Specialty
                                          }).ToArray(),
                    Series = new Series
                    {
                        HattrickId = team.SeriesHattrickId,
                        Name = team.SeriesName
                    },
                    UpcomingMatches = team.OverviewMatches.Where(y => y.Status == MatchStatus.Upcoming)
                                                          .OrderBy(y => y.StartsOn)
                                                          .Select(y => new UpcomingMatch
                                                          {
                                                              Date = y.StartsOn,
                                                              HomeTeam = new Team
                                                              {
                                                                  HattrickId = y.HomeTeamHattrickId,
                                                                  Name = y.HomeTeamName
                                                              },
                                                              AwayTeam = new Team
                                                              {
                                                                  HattrickId = y.AwayTeamHattrickId,
                                                                  Name = y.AwayTeamName
                                                              },
                                                              Type = y.Type
                                                          }).ToArray(),
                }
            };
        }
    }
}