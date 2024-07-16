namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class TeamDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> seniorTeamRepository;

        public TeamDetails(
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IHattrickRepository<Domain.Region> regionRepository,
            IHattrickRepository<Domain.Senior.Team> seniorTeamRepository)
        {
            this.leagueRepository = leagueRepository;
            this.managerRepository = managerRepository;
            this.regionRepository = regionRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.TeamDetails.HattrickData file)
            {
                var xmlTeamIds = file.Teams.Select(x => x.TeamId)
                    .ToArray();

                var seniorTeamIds = await this.seniorTeamRepository.Query()
                    .Select(x => x.HattrickId)
                    .ToArrayAsync(cancellationToken);

                var seniorTeamIdsToDelete = seniorTeamIds.Except(xmlTeamIds)
                    .ToArray();

                if (seniorTeamIdsToDelete.Length > 0)
                {
                    await this.seniorTeamRepository.DeleteRangeAsync(seniorTeamIdsToDelete);
                }

                var manager = await this.managerRepository.GetByHattrickIdAsync(file.User.UserId);

                ArgumentNullException.ThrowIfNull(manager, nameof(manager));

                foreach (var xmlTeam in file.Teams)
                {
                    await this.ProcessSeniorTeamAsync(xmlTeam, manager, cancellationToken);
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task ProcessSeniorTeamAsync(
            Models.TeamDetails.Team xmlTeam,
            Domain.Manager manager,
            CancellationToken cancellationToken)
        {
            var region = await this.regionRepository.GetByHattrickIdAsync(xmlTeam.Region.Id);

            ArgumentNullException.ThrowIfNull(region, nameof(region));

            var league = await this.leagueRepository.GetByHattrickIdAsync(xmlTeam.League.Id);

            ArgumentNullException.ThrowIfNull(league, nameof(league));

            var team = await this.seniorTeamRepository.GetByHattrickIdAsync(xmlTeam.TeamId);

            var logoBytes = await GetImageBytesFromCacheAsync(
                xmlTeam.LogoUrl,
                cancellationToken);

            var homeMatchKitBytes = await GetImageBytesFromCacheAsync(
                xmlTeam.DressUri,
                cancellationToken);

            var awayMatchKitBytes = await GetImageBytesFromCacheAsync(
                xmlTeam.DressAlternateUri,
                cancellationToken);

            if (team == null)
            {
                await this.seniorTeamRepository.InsertAsync(
                    new Domain.Senior.Team
                    {
                        HattrickId = xmlTeam.TeamId,
                        Name = xmlTeam.TeamName,
                        ShortName = xmlTeam.ShortTeamName,
                        FoundedOn = xmlTeam.FoundedDate,
                        IsPrimary = xmlTeam.IsPrimaryClub,
                        IsPlayingCup = xmlTeam.Cup?.StillInCup ?? false,
                        GlobalRanking = xmlTeam.PowerRating.GlobalRanking,
                        LeagueRanking = xmlTeam.PowerRating.LeagueRanking,
                        RegionRanking = xmlTeam.PowerRating.RegionRanking,
                        TeamRanking = xmlTeam.TeamRank,
                        PowerRating = xmlTeam.PowerRating.PowerRating,
                        UndefeatedStreak = xmlTeam.NumberOfUndefeated ?? 0,
                        WinningStreak = xmlTeam.NumberOfVictories ?? 0,
                        CanBookMidWeekFriendly = xmlTeam.PossibleToChallengeMidweek,
                        CanBookWeekEndFriendly = xmlTeam.PossibleToChallengeWeekend,
                        LogoBytes = logoBytes,
                        HomeMatchKitBytes = homeMatchKitBytes,
                        AwayMatchKitBytes = awayMatchKitBytes,
                        League = league,
                        Manager = manager,
                        Region = region
                    });
            }
            else
            {
                team.Name = xmlTeam.TeamName;
                team.ShortName = xmlTeam.ShortTeamName;
                team.IsPlayingCup = xmlTeam.Cup?.StillInCup ?? false;
                team.GlobalRanking = xmlTeam.PowerRating.GlobalRanking;
                team.LeagueRanking = xmlTeam.PowerRating.LeagueRanking;
                team.RegionRanking = xmlTeam.PowerRating.RegionRanking;
                team.TeamRanking = xmlTeam.TeamRank;
                team.PowerRating = xmlTeam.PowerRating.PowerRating;
                team.UndefeatedStreak = xmlTeam.NumberOfUndefeated ?? 0;
                team.WinningStreak = xmlTeam.NumberOfVictories ?? 0;
                team.CanBookMidWeekFriendly = xmlTeam.PossibleToChallengeMidweek;
                team.CanBookWeekEndFriendly = xmlTeam.PossibleToChallengeWeekend;
                team.LogoBytes = logoBytes;
                team.HomeMatchKitBytes = homeMatchKitBytes;
                team.AwayMatchKitBytes = awayMatchKitBytes;
                team.Region = region;

                this.seniorTeamRepository.Update(team);
            }
        }
    }
}