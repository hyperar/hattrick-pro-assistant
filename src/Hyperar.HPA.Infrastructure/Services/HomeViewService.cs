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
        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public HomeViewService(IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task<TeamOverview> GetTeamsOverview(uint seniorTeamId)
        {
            var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(seniorTeamId);

            ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

            return new TeamOverview
            {
                Country = new Country
                {
                    HattrickId = seniorTeam.Region.Country.HattrickId,
                    Name = seniorTeam.Region.Country.Name,
                },
                Manager = new Manager
                {
                    HattrickId = seniorTeam.Manager.HattrickId,
                    UserName = seniorTeam.Manager.UserName,
                    SupporterTier = seniorTeam.Manager.SupporterTier
                },
                PlayedMatches = seniorTeam.OverviewMatches.Where(y => y.Status == MatchStatus.Finished)
                                                          .OrderBy(y => y.StartsOn)
                                                          .Select(y => new PlayedMatch
                                                          {
                                                              Date = y.StartsOn,
                                                              HomeTeam = new SeniorTeam
                                                              {
                                                                  HattrickId = y.HomeTeamHattrickId,
                                                                  Name = y.HomeTeamName
                                                              },
                                                              HomeGoals = y.HomeGoals ?? 0,
                                                              AwayTeam = new SeniorTeam
                                                              {
                                                                  HattrickId = y.AwayTeamHattrickId,
                                                                  Name = y.AwayTeamName
                                                              },
                                                              AwayGoals = y.AwayGoals ?? 0,
                                                              Type = y.Type
                                                          }).ToArray(),
                Region = new Region
                {
                    HattrickId = seniorTeam.Region.HattrickId,
                    Name = seniorTeam.Region.Name
                },
                SeniorPlayers = seniorTeam.SeniorPlayers.OrderBy(x => x.ShirtNumber)
                                                        .ThenBy(x => x.LastName)
                                                        .ThenBy(x => x.FirstName)
                                                        .ThenBy(x => x.HattrickId)
                                                        .Select(x => new SeniorPlayer
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
                SeniorSeries = new SeniorSeries
                {
                    HattrickId = seniorTeam.SeniorSeriesHattrickId,
                    Name = seniorTeam.SeniorSeriesName
                },
                SeniorTeam = new SeniorTeam
                {
                    HattrickId = seniorTeam.HattrickId,
                    Name = seniorTeam.Name,
                    Logo = seniorTeam.Logo
                },
                UpcomingMatches = seniorTeam.OverviewMatches.Where(y => y.Status == MatchStatus.Upcoming)
                                                            .OrderBy(y => y.StartsOn)
                                                            .Select(y => new UpcomingMatch
                                                            {
                                                                Date = y.StartsOn,
                                                                HomeTeam = new SeniorTeam
                                                                {
                                                                    HattrickId = y.HomeTeamHattrickId,
                                                                    Name = y.HomeTeamName
                                                                },
                                                                AwayTeam = new SeniorTeam
                                                                {
                                                                    HattrickId = y.AwayTeamHattrickId,
                                                                    Name = y.AwayTeamName
                                                                },
                                                                Type = y.Type
                                                            }).ToArray(),
            };
        }
    }
}