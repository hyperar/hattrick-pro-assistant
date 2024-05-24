namespace Hyperar.HPA.Infrastructure.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Services;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Shared.Models.UI.Home;

    public class HomeViewService : IHomeViewService
    {
        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        private readonly IRepository<Domain.User> userRepository;

        public HomeViewService(
            IHattrickRepository<Domain.Senior.Team> teamRepository,
            IRepository<Domain.User> userRepository)
        {
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<Currency> GetManagerCurrencyAsync()
        {
            Domain.User? user = await this.userRepository.Query().SingleOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNull(user.Manager, nameof(user.Manager));

            return new Currency
            {
                Name = user.Manager.CurrencyName,
                Rate = user.Manager.CurrencyRate
            };
        }

        public async Task<Team> GetTeamsOverviewAsync(long teamId)
        {
            var team = await this.teamRepository.GetByHattrickIdAsync(teamId);

            ArgumentNullException.ThrowIfNull(team, nameof(team));

            return new Team
            {
                HattrickId = team.HattrickId,
                Name = team.Name,
                LogoBytes = team.LogoBytes,
                HomeMatchKitBytes = team.HomeMatchKitBytes,
                AwayMatchKitBytes = team.AwayMatchKitBytes,
                Manager = new Manager
                {
                    HattrickId = team.Manager.HattrickId,
                    UserName = team.Manager.UserName,
                    SupporterTier = team.Manager.SupporterTier,
                    AvatarBytes = team.Manager.AvatarBytes,
                    Country = new Country
                    {
                        HattrickId = team.Manager.Country.HattrickId,
                        Name = team.Manager.Country.Name,
                        FlagBytes = team.Manager.Country.League.FlagBytes,
                    },
                },
                League = new League
                {
                    HattrickId = team.League.HattrickId,
                    Name = team.League.Name,
                    FlagBytes = team.League.FlagBytes,
                },
                Country = new Country
                {
                    HattrickId = team.Region.Country.HattrickId,
                    Name = team.Region.Country.Name,
                    FlagBytes = team.Region.Country.League.FlagBytes,
                },
                Region = new Region
                {
                    HattrickId = team.Region.HattrickId,
                    Name = team.Region.Name
                },
                Players = team.Players.Where(x => !x.IsCoach
                                               && (x.IsTransferListed ||
                                                   x.Health > -1 ||
                                                   x.BookingStatus != BookingStatus.NoBookings))
                                      .OrderBy(x => x.ShirtNumber)
                                      .ThenBy(x => x.LastName)
                                      .ThenBy(x => x.FirstName)
                                      .ThenBy(x => x.HattrickId)
                                      .Select(x => new Player
                                      {
                                          HattrickId = x.HattrickId,
                                          FirstName = x.FirstName,
                                          NickName = x.NickName,
                                          LastName = x.LastName,
                                          AskingPrice = (long)((x.AskingPrice ?? 0) / team.Manager.CurrencyRate),
                                          BookingStatus = x.BookingStatus,
                                          HasMotherClubBonus = x.HasMotherClubBonus,
                                          IsTransferListed = x.IsTransferListed,
                                          HealthStatus = x.Health,
                                          ShirtNumber = x.ShirtNumber,
                                          Specialty = x.Specialty,
                                          WinningBid = (long)((x.WinningBid ?? 0) / team.Manager.CurrencyRate),
                                      }).ToArray(),
                Series = new Series
                {
                    HattrickId = team.SeriesHattrickId,
                    Name = team.SeriesName
                },
                RecentMatches = team.Matches.Where(x => x.FinishDate.HasValue)
                                            .OrderByDescending(x => x.StartDate)
                                            .Take(5)
                                            .Select(x => new RecentMatch
                                            {
                                                Date = x.StartDate,
                                                HattrickId = x.HattrickId,
                                                AwayTeam = new MatchTeam
                                                {
                                                    HattrckId = x.Teams.Where(x => x.Location == MatchTeamLocation.Away)
                                                                       .Single().HattrickId,
                                                    Name = x.Teams.Where(x => x.Location == MatchTeamLocation.Away)
                                                                  .Single().Name
                                                },
                                                AwayTeamScore = x.Teams.Where(x => x.Location == MatchTeamLocation.Away)
                                                                       .Single().Score ?? 0,
                                                HomeTeam = new MatchTeam
                                                {
                                                    HattrckId = x.Teams.Where(x => x.Location == MatchTeamLocation.Home)
                                                                       .Single().HattrickId,
                                                    Name = x.Teams.Where(x => x.Location == MatchTeamLocation.Home)
                                                                  .Single().Name
                                                },
                                                HomeTeamScore = x.Teams.Where(x => x.Location == MatchTeamLocation.Home)
                                                                       .Single().Score ?? 0,
                                                Result = x.Result ?? MatchResult.Drawn,
                                                Type = x.Type
                                            }).ToArray(),
                UpcomingMatches = team.Matches.Where(x => !x.FinishDate.HasValue)
                                              .OrderByDescending(x => x.StartDate)
                                              .Take(5)
                                              .OrderBy(x => x.StartDate)
                                              .Select(x => new Match
                                              {
                                                  Date = x.StartDate,
                                                  HattrickId = x.HattrickId,
                                                  AwayTeam = new MatchTeam
                                                  {
                                                      HattrckId = x.Teams.Where(x => x.Location == MatchTeamLocation.Away)
                                                                       .Single().HattrickId,
                                                      Name = x.Teams.Where(x => x.Location == MatchTeamLocation.Away)
                                                                  .Single().Name
                                                  },
                                                  HomeTeam = new MatchTeam
                                                  {
                                                      HattrckId = x.Teams.Where(x => x.Location == MatchTeamLocation.Home)
                                                                       .Single().HattrickId,
                                                      Name = x.Teams.Where(x => x.Location == MatchTeamLocation.Home)
                                                                  .Single().Name
                                                  },
                                                  Type = x.Type
                                              }).ToArray(),
            };
        }
    }
}