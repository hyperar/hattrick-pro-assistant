namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Domain.Interfaces;
    using Hattrick = Hyperar.HPA.Application.Hattrick.WorldDetails;

    public class WorldDetails : IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext context;

        private readonly IHattrickRepository<Domain.Country> countryRepository;

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
            this.context = databaseContext;
            this.countryRepository = countryRepository;
            this.leagueCupRepository = leagueCupRepository;
            this.leagueRepository = leagueRepository;
            this.regionRepository = regionRepository;
        }

        public void PersistData(IXmlFile file)
        {
            var entity = (Hattrick.HattrickData)file;

            this.ProcessWorldDetails(entity);
        }

        private void ProcessCountry(Hattrick.Country xmlCountry, uint leagueId)
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

            var country = this.countryRepository.GetByHattrickId(xmlCountry.CountryId.Value);

            if (country == null)
            {
                country = new Domain.Country
                {
                    LeagueHattrickId = leagueId,
                    HattrickId = xmlCountry.CountryId.Value,
                    Name = xmlCountry.CountryName,
                    CurrencyName = xmlCountry.CurrencyName,
                    CurrencyRate = xmlCountry.CurrencyRate.Value,
                    Code = xmlCountry.CountryCode,
                    DateFormat = xmlCountry.DateFormat,
                    TimeFormat = xmlCountry.TimeFormat
                };

                this.countryRepository.Insert(country);
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

            this.context.Save();

            if (xmlCountry.RegionList != null)
            {
                foreach (var curXmlRegion in xmlCountry.RegionList)
                {
                    this.ProcessRegion(curXmlRegion, country.HattrickId);
                }

                this.context.Save();
            }
        }

        private void ProcessLeague(Hattrick.League xmlLeague)
        {
            var league = this.leagueRepository.GetByHattrickId(xmlLeague.LeagueId);

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
                    SeasonOffset = xmlLeague.SeasonOffset,
                    CurrentRound = xmlLeague.MatchRound,
                    LanguageId = xmlLeague.LanguageId,
                    LanguageName = xmlLeague.LanguageName,
                    SeniorNationalTeamId = xmlLeague.NationalTeamId,
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
                    FifthWeeklyUpdate = xmlLeague.Sequence7
                };

                this.leagueRepository.Insert(league);
            }
            else
            {
                league.Season = xmlLeague.Season;
                league.SeasonOffset = xmlLeague.SeasonOffset;
                league.CurrentRound = xmlLeague.MatchRound;
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

            this.context.Save();

            this.ProcessCountry(xmlLeague.Country, xmlLeague.LeagueId);

            if (xmlLeague.Cups != null)
            {
                foreach (var curXmlCup in xmlLeague.Cups)
                {
                    this.ProcessLeagueCup(curXmlCup, xmlLeague.LeagueId);
                }

                this.context.Save();
            }
        }

        private void ProcessLeagueCup(Hattrick.Cup xmlCup, uint leagueId)
        {
            var cup = this.leagueCupRepository.GetByHattrickId(xmlCup.CupId);

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

                this.leagueCupRepository.Insert(cup);
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

        private void ProcessRegion(Hattrick.Region xmlRegion, uint countryId)
        {
            var region = this.regionRepository.GetByHattrickId(xmlRegion.RegionId);

            if (region == null)
            {
                region = new Domain.Region
                {
                    CountryHattrickId = countryId,
                    HattrickId = xmlRegion.RegionId,
                    Name = xmlRegion.RegionName
                };

                regionRepository.Insert(region);
            }
            else
            {
                region.Name = xmlRegion.RegionName;

                this.regionRepository.Update(region);
            }
        }

        private void ProcessWorldDetails(Hattrick.HattrickData entity)
        {
            this.context.BeginTransaction();

            try
            {
                foreach (var curXmlLeague in entity.LeagueList)
                {
                    this.ProcessLeague(curXmlLeague);
                }
            }
            catch
            {
                this.context.Cancel();

                throw;
            }
            finally
            {
                this.context.EndTransaction();
            }
        }
    }
}