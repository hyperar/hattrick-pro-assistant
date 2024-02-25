namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Hattrick = Application.Hattrick.WorldDetails;

    public class WorldDetails : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private const string flagUrlMask = "https://www.hattrick.org/Img/flags/{0}.png";

        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.LeagueCup> leagueCupRepository;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        public WorldDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.LeagueCup> leagueCupRepository,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Region> regionRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.leagueCupRepository = leagueCupRepository;
            this.leagueRepository = leagueRepository;
            this.regionRepository = regionRepository;
        }

        public override async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessWorldDetailsAsync(xmlEntity);
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

        private async Task ProcessCountryAsync(Hattrick.Country xmlCountry, Domain.League league)
        {
            if (!xmlCountry.Available ||
                xmlCountry.CountryId == null ||
                xmlCountry.CountryName == null ||
                xmlCountry.CurrencyName == null ||
                xmlCountry.CurrencyRate == null ||
                xmlCountry.CountryCode == null ||
                xmlCountry.DateFormat == null ||
                xmlCountry.TimeFormat == null)
            {
                return;
            }

            Domain.Country? country = await this.countryRepository.GetByHattrickIdAsync(xmlCountry.CountryId.Value);

            if (country == null)
            {
                country = new Domain.Country
                {
                    League = league,
                    HattrickId = xmlCountry.CountryId.Value,
                    Name = xmlCountry.CountryName,
                    CurrencyName = xmlCountry.CurrencyName,
                    CurrencyRate = xmlCountry.CurrencyRate.Value,
                    Code = xmlCountry.CountryCode,
                    DateFormat = xmlCountry.DateFormat,
                    TimeFormat = xmlCountry.TimeFormat
                };

                await this.countryRepository.InsertAsync(country);
            }
            else
            {
                country.Name = xmlCountry.CountryName;
                country.CurrencyName = xmlCountry.CurrencyName;
                country.CurrencyRate = xmlCountry.CurrencyRate.Value;
                country.Code = xmlCountry.CountryCode;
                country.DateFormat = xmlCountry.DateFormat;
                country.TimeFormat = xmlCountry.TimeFormat;

                this.countryRepository.Update(country);
            }

            await this.databaseContext.SaveAsync();

            if (xmlCountry.RegionList != null && xmlCountry.RegionList.Count > 0)
            {
                foreach (Hattrick.Region curXmlRegion in xmlCountry.RegionList)
                {
                    await this.ProcessRegionAsync(curXmlRegion, country);
                }

                await this.databaseContext.SaveAsync();
            }
        }

        private async Task ProcessLeagueAsync(Hattrick.League xmlLeague)
        {
            Domain.League? league = await this.leagueRepository.GetByHattrickIdAsync(xmlLeague.LeagueId);

            if (league == null)
            {
                league = new Domain.League
                {
                    HattrickId = xmlLeague.LeagueId,
                    Name = xmlLeague.LeagueName,
                    ShortName = xmlLeague.ShortName,
                    EnglishName = xmlLeague.EnglishName,
                    Continent = xmlLeague.Continent,
                    Zone = xmlLeague.ZoneName,
                    Season = xmlLeague.Season,
                    Week = xmlLeague.MatchRound,
                    SeasonOffset = xmlLeague.SeasonOffset,
                    LanguageId = xmlLeague.LanguageId,
                    LanguageName = xmlLeague.LanguageName,
                    NationalTeamId = xmlLeague.NationalTeamId,
                    JuniorNationalTeamId = xmlLeague.U20TeamId,
                    ActiveTeams = xmlLeague.ActiveTeams,
                    ActiveUsers = xmlLeague.ActiveUsers,
                    WaitingUsers = xmlLeague.WaitingUsers,
                    NumberOfLevels = xmlLeague.NumberOfLevels,
                    NextTrainingUpdate = xmlLeague.TrainingDate,
                    NextEconomyUpdate = xmlLeague.EconomyDate,
                    NextCupMatchDate = xmlLeague.CupMatchDate,
                    NextSeriesMatchDate = xmlLeague.SeriesMatchDate,
                    FirstWeeklyUpdate = xmlLeague.Sequence1,
                    SecondWeeklyUpdate = xmlLeague.Sequence2,
                    ThirdWeeklyUpdate = xmlLeague.Sequence3,
                    FourthWeeklyUpdate = xmlLeague.Sequence5,
                    FifthWeeklyUpdate = xmlLeague.Sequence7,
                    FlagBytes = await DownloadWebResourceAsync(
                        string.Format(
                            flagUrlMask,
                            xmlLeague.LeagueId))
                };

                await this.leagueRepository.InsertAsync(league);
            }
            else
            {
                league.Season = xmlLeague.Season;
                league.Week = xmlLeague.MatchRound;
                league.SeasonOffset = xmlLeague.SeasonOffset;
                league.ActiveTeams = xmlLeague.ActiveTeams;
                league.ActiveUsers = xmlLeague.ActiveUsers;
                league.WaitingUsers = xmlLeague.WaitingUsers;
                league.NumberOfLevels = xmlLeague.NumberOfLevels;
                league.NextTrainingUpdate = xmlLeague.TrainingDate;
                league.NextEconomyUpdate = xmlLeague.EconomyDate;
                league.NextCupMatchDate = xmlLeague.CupMatchDate;
                league.NextSeriesMatchDate = xmlLeague.SeriesMatchDate;
                league.FirstWeeklyUpdate = xmlLeague.Sequence1;
                league.SecondWeeklyUpdate = xmlLeague.Sequence2;
                league.ThirdWeeklyUpdate = xmlLeague.Sequence3;
                league.FourthWeeklyUpdate = xmlLeague.Sequence5;
                league.FifthWeeklyUpdate = xmlLeague.Sequence7;

                this.leagueRepository.Update(league);
            }

            await this.databaseContext.SaveAsync();

            await this.ProcessCountryAsync(xmlLeague.Country, league);

            if (xmlLeague.Cups != null && xmlLeague.Cups.Count > 0)
            {
                foreach (Hattrick.Cup curXmlCup in xmlLeague.Cups)
                {
                    await this.ProcessLeagueCupAsync(curXmlCup, xmlLeague.LeagueId);
                }

                await this.databaseContext.SaveAsync();
            }
        }

        private async Task ProcessLeagueCupAsync(Hattrick.Cup xmlCup, uint leagueId)
        {
            Domain.LeagueCup? cup = await this.leagueCupRepository.GetByHattrickIdAsync(xmlCup.CupId);

            if (cup == null)
            {
                cup = new Domain.LeagueCup
                {
                    LeagueHattrickId = leagueId,
                    HattrickId = xmlCup.CupId,
                    Name = xmlCup.CupName,
                    LeagueLevel = xmlCup.CupLeagueLevel,
                    Level = xmlCup.CupLevel,
                    LevelIndex = xmlCup.CupLevelIndex,
                    CurrentRound = xmlCup.MatchRound,
                    RoundsLeft = xmlCup.MatchRoundsLeft
                };

                await this.leagueCupRepository.InsertAsync(cup);
            }
            else
            {
                cup.Name = xmlCup.CupName;
                cup.LeagueLevel = xmlCup.CupLeagueLevel;
                cup.Level = xmlCup.CupLevel;
                cup.LevelIndex = xmlCup.CupLevelIndex;
                cup.CurrentRound = xmlCup.MatchRound;
                cup.RoundsLeft = xmlCup.MatchRoundsLeft;

                this.leagueCupRepository.Update(cup);
            }
        }

        private async Task ProcessRegionAsync(Hattrick.Region xmlRegion, Domain.Country country)
        {
            Domain.Region? region = await this.regionRepository.GetByHattrickIdAsync(xmlRegion.RegionId);

            if (region == null)
            {
                region = new Domain.Region
                {
                    Country = country,
                    HattrickId = xmlRegion.RegionId,
                    Name = xmlRegion.RegionName
                };

                await this.regionRepository.InsertAsync(region);
            }
            else
            {
                region.Name = xmlRegion.RegionName;

                this.regionRepository.Update(region);
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessWorldDetailsAsync(Hattrick.HattrickData xmlEntity)
        {
            foreach (Hattrick.League curXmlLeague in xmlEntity.LeagueList)
            {
                await this.ProcessLeagueAsync(curXmlLeague);
            }
        }
    }
}