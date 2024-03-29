﻿namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
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

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public TeamDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IHattrickRepository<Domain.Region> regionRepository,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.databaseContext = databaseContext;
            this.leagueRepository = leagueRepository;
            this.managerRepository = managerRepository;
            this.regionRepository = regionRepository;
            this.seniorTeamRepository = seniorTeamRepository;
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
            var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(xmlTeam.TeamId);

            var league = await this.leagueRepository.GetByHattrickIdAsync(xmlTeam.League.LeagueId);

            ArgumentNullException.ThrowIfNull(league, nameof(league));

            var region = await this.regionRepository.GetByHattrickIdAsync(xmlTeam.Region.RegionId);

            ArgumentNullException.ThrowIfNull(region, nameof(region));

            if (seniorTeam == null)
            {
                seniorTeam = new Domain.SeniorTeam
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
                    SeniorSeriesHattrickId = xmlTeam.LeagueLevelUnit.LeagueLevelUnitId,
                    SeniorSeriesName = xmlTeam.LeagueLevelUnit.LeagueLevelUnitName,
                    SeniorSeriesDivision = xmlTeam.LeagueLevelUnit.LeagueLevel,
                    LogoUrl = !string.IsNullOrWhiteSpace(xmlTeam.LogoUrl) ? NormalizeUrl(xmlTeam.LogoUrl) : null,
                    MatchKitUrl = NormalizeUrl(xmlTeam.DressUri),
                    AlternativeMatchKitUrl = NormalizeUrl(xmlTeam.DressAlternateUri),
                    Logo = !string.IsNullOrWhiteSpace(xmlTeam.LogoUrl) ? await DownloadWebResource(xmlTeam.LogoUrl) : null,
                    MatchKit = await DownloadWebResource(xmlTeam.DressUri),
                    AlternativeMatchKit = await DownloadWebResource(xmlTeam.DressAlternateUri),
                    League = league,
                    Manager = manager,
                    Region = region
                };

                await this.seniorTeamRepository.InsertAsync(seniorTeam);
            }
            else
            {
                seniorTeam.HattrickId = xmlTeam.TeamId;
                seniorTeam.Name = xmlTeam.TeamName;
                seniorTeam.ShortName = xmlTeam.ShortTeamName;
                seniorTeam.IsPrimary = xmlTeam.IsPrimaryClub;
                seniorTeam.FoundedOn = xmlTeam.FoundedDate;
                seniorTeam.CoachPlayerId = xmlTeam.Trainer.PlayerId;
                seniorTeam.IsPlayingCup = xmlTeam.Cup != null && xmlTeam.Cup.StillInCup;
                seniorTeam.GlobalRanking = xmlTeam.PowerRating.GlobalRanking;
                seniorTeam.LeagueRanking = xmlTeam.PowerRating.LeagueRanking;
                seniorTeam.RegionRanking = xmlTeam.PowerRating.RegionRanking;
                seniorTeam.PowerRanking = xmlTeam.PowerRating.PowerRating;
                seniorTeam.TeamRank = xmlTeam.TeamRank ?? 0;
                seniorTeam.UndefeatedStreak = xmlTeam.NumberOfUndefeated ?? 0;
                seniorTeam.WinStreak = xmlTeam.NumberOfVictories ?? 0;
                seniorTeam.SeniorSeriesHattrickId = xmlTeam.LeagueLevelUnit.LeagueLevelUnitId;
                seniorTeam.SeniorSeriesName = xmlTeam.LeagueLevelUnit.LeagueLevelUnitName;
                seniorTeam.SeniorSeriesDivision = xmlTeam.LeagueLevelUnit.LeagueLevel;

                if (string.IsNullOrWhiteSpace(xmlTeam.LogoUrl) && !string.IsNullOrWhiteSpace(seniorTeam.LogoUrl))
                {
                    seniorTeam.Logo = null;
                    seniorTeam.LogoUrl = null;
                }
                else if (!string.IsNullOrWhiteSpace(xmlTeam.LogoUrl) && NormalizeUrl(xmlTeam.LogoUrl) != seniorTeam.LogoUrl)
                {
                    seniorTeam.LogoUrl = NormalizeUrl(xmlTeam.LogoUrl);
                    seniorTeam.Logo = await DownloadWebResource(xmlTeam.LogoUrl);
                }

                string matchKitUrl = NormalizeUrl(xmlTeam.DressUri);

                if (matchKitUrl != seniorTeam.MatchKitUrl)
                {
                    seniorTeam.MatchKitUrl = matchKitUrl;
                    seniorTeam.MatchKit = await DownloadWebResource(matchKitUrl);
                }

                string alternativeMatchKitUrl = NormalizeUrl(xmlTeam.DressAlternateUri);

                if (alternativeMatchKitUrl != seniorTeam.AlternativeMatchKitUrl)
                {
                    seniorTeam.AlternativeMatchKitUrl = alternativeMatchKitUrl;
                    seniorTeam.AlternativeMatchKit = await DownloadWebResource(alternativeMatchKitUrl);
                }

                seniorTeam.Region = region;
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