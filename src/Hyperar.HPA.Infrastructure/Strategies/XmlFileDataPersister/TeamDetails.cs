namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Hattrick = Application.Hattrick.TeamDetails;

    public class TeamDetails : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public TeamDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IHattrickRepository<Domain.Region> regionRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.leagueRepository = leagueRepository;
            this.managerRepository = managerRepository;
            this.regionRepository = regionRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessTeamDetailsAsync(xmlEntity);
                }
                else
                {
                    throw new ArgumentException(file.GetType().FullName, nameof(file));
                }
            }
            catch
            {
                this.databaseContext.Cancel();

                throw;
            }
        }

        private async Task ProcessTeamAsync(Hattrick.Team xmlTeam, Domain.Manager manager)
        {
            var team = await this.teamRepository.GetByHattrickIdAsync(xmlTeam.TeamId);

            var league = await this.leagueRepository.GetByHattrickIdAsync(xmlTeam.League.LeagueId);

            ArgumentNullException.ThrowIfNull(league, nameof(league));

            var region = await this.regionRepository.GetByHattrickIdAsync(xmlTeam.Region.RegionId);

            ArgumentNullException.ThrowIfNull(region, nameof(region));

            if (team == null)
            {
                team = new Domain.Senior.Team
                {
                    HattrickId = xmlTeam.TeamId,
                    Name = xmlTeam.TeamName,
                    ShortName = xmlTeam.ShortTeamName,
                    IsPrimary = xmlTeam.IsPrimaryClub,
                    FoundedOn = xmlTeam.FoundedDate,
                    CoachPlayerId = xmlTeam.Trainer.PlayerId,
                    IsPlayingCup = xmlTeam.Cup != null && xmlTeam.Cup.StillInCup,
                    GlobalRanking = xmlTeam.PowerRating.GlobalRanking,
                    LeagueRanking = xmlTeam.PowerRating.LeagueRanking,
                    RegionRanking = xmlTeam.PowerRating.RegionRanking,
                    PowerRanking = xmlTeam.PowerRating.PowerRating,
                    TeamRank = xmlTeam.TeamRank ?? 0,
                    UndefeatedStreak = xmlTeam.NumberOfUndefeated ?? 0,
                    WinStreak = xmlTeam.NumberOfVictories ?? 0,
                    SeriesHattrickId = xmlTeam.LeagueLevelUnit.LeagueLevelUnitId,
                    SeriesName = xmlTeam.LeagueLevelUnit.LeagueLevelUnitName,
                    SeriesDivision = xmlTeam.LeagueLevelUnit.LeagueLevel,
                    LogoUrl = !string.IsNullOrWhiteSpace(xmlTeam.LogoUrl) ? NormalizeUrl(xmlTeam.LogoUrl) : null,
                    MatchKitUrl = NormalizeUrl(xmlTeam.DressUri),
                    AlternativeMatchKitUrl = NormalizeUrl(xmlTeam.DressAlternateUri),
                    LogoBytes = !string.IsNullOrWhiteSpace(xmlTeam.LogoUrl)
                         ? await DownloadWebResourceAsync(
                             NormalizeUrl(
                                 xmlTeam.LogoUrl))
                         : null,
                    MatchKitBytes = await DownloadWebResourceAsync(
                        NormalizeUrl(xmlTeam.DressUri)),
                    AlternativeMatchKitBytes = await DownloadWebResourceAsync(
                        NormalizeUrl(xmlTeam.DressAlternateUri)),
                    League = league,
                    Manager = manager,
                    Region = region
                };

                await this.teamRepository.InsertAsync(team);
            }
            else
            {
                team.HattrickId = xmlTeam.TeamId;
                team.Name = xmlTeam.TeamName;
                team.ShortName = xmlTeam.ShortTeamName;
                team.IsPrimary = xmlTeam.IsPrimaryClub;
                team.FoundedOn = xmlTeam.FoundedDate;
                team.CoachPlayerId = xmlTeam.Trainer.PlayerId;
                team.IsPlayingCup = xmlTeam.Cup != null && xmlTeam.Cup.StillInCup;
                team.GlobalRanking = xmlTeam.PowerRating.GlobalRanking;
                team.LeagueRanking = xmlTeam.PowerRating.LeagueRanking;
                team.RegionRanking = xmlTeam.PowerRating.RegionRanking;
                team.PowerRanking = xmlTeam.PowerRating.PowerRating;
                team.TeamRank = xmlTeam.TeamRank ?? 0;
                team.UndefeatedStreak = xmlTeam.NumberOfUndefeated ?? 0;
                team.WinStreak = xmlTeam.NumberOfVictories ?? 0;
                team.SeriesHattrickId = xmlTeam.LeagueLevelUnit.LeagueLevelUnitId;
                team.SeriesName = xmlTeam.LeagueLevelUnit.LeagueLevelUnitName;
                team.SeriesDivision = xmlTeam.LeagueLevelUnit.LeagueLevel;

                if (string.IsNullOrWhiteSpace(xmlTeam.LogoUrl) && !string.IsNullOrWhiteSpace(team.LogoUrl))
                {
                    team.LogoBytes = null;
                    team.LogoUrl = null;
                }
                else if (!string.IsNullOrWhiteSpace(xmlTeam.LogoUrl) && NormalizeUrl(xmlTeam.LogoUrl) != team.LogoUrl)
                {
                    team.LogoUrl = NormalizeUrl(xmlTeam.LogoUrl);
                    team.LogoBytes = await DownloadWebResourceAsync(xmlTeam.LogoUrl);
                }

                string matchKitUrl = NormalizeUrl(xmlTeam.DressUri);

                if (matchKitUrl != team.MatchKitUrl)
                {
                    team.MatchKitUrl = matchKitUrl;
                    team.MatchKitBytes = await DownloadWebResourceAsync(matchKitUrl);
                }

                string alternativeMatchKitUrl = NormalizeUrl(xmlTeam.DressAlternateUri);

                if (alternativeMatchKitUrl != team.AlternativeMatchKitUrl)
                {
                    team.AlternativeMatchKitUrl = alternativeMatchKitUrl;
                    team.AlternativeMatchKitBytes = await DownloadWebResourceAsync(alternativeMatchKitUrl);
                }

                team.Region = region;
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessTeamDetailsAsync(Hattrick.HattrickData xmlEntity)
        {
            var manager = await this.managerRepository.GetByHattrickIdAsync(xmlEntity.User.UserId);

            ArgumentNullException.ThrowIfNull(manager, nameof(manager));

            foreach (var curXmlTeam in xmlEntity.Teams)
            {
                await this.ProcessTeamAsync(curXmlTeam, manager);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}