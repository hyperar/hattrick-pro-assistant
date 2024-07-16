namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick;

    public class WorldDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private const string LeagueFlagImageUrlMask = "/Img/flags/{0}.png";

        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IRepository<Domain.Currency> currencyRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.LeagueCup> leagueCupRepository;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IRepository<Domain.LeagueSchedule> leagueScheduleRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        public WorldDetails(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IRepository<Domain.Currency> currencyRepository,
            IHattrickRepository<Domain.LeagueCup> leagueCupRepository,
            IHattrickRepository<Domain.League> leagueRepository,
            IRepository<Domain.LeagueSchedule> leagueScheduleRepository,
            IHattrickRepository<Domain.Region> regionRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.currencyRepository = currencyRepository;
            this.leagueCupRepository = leagueCupRepository;
            this.leagueRepository = leagueRepository;
            this.leagueScheduleRepository = leagueScheduleRepository;
            this.regionRepository = regionRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.WorldDetails.HattrickData file)
            {
                foreach (var xmlLeague in file.LeagueList)
                {
                    var league = await this.ProcessLeagueAsync(xmlLeague, cancellationToken);

                    await this.ProcessLeagueScheduleAsync(xmlLeague, league, cancellationToken);

                    foreach (var xmlCup in xmlLeague.Cups)
                    {
                        await this.ProcessLeagueCupAsync(xmlCup, league, cancellationToken);
                    }

                    if (xmlLeague.Country.Available)
                    {
                        var currency = await this.ProcessCurrencyAsync(xmlLeague.Country, cancellationToken);

                        var country = await this.ProcessCountryAsync(xmlLeague.Country, currency, league, cancellationToken);

                        if (xmlLeague.Country.RegionList.Count > 0)
                        {
                            foreach (var xmlRegion in xmlLeague.Country.RegionList)
                            {
                                await this.ProcessRegionAsync(xmlRegion, country, cancellationToken);
                            }
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task<Domain.Country> ProcessCountryAsync(
            Models.WorldDetails.Country xmlCountry,
            Domain.Currency currency,
            Domain.League league,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryId, nameof(xmlCountry.CountryId));
            ArgumentException.ThrowIfNullOrWhiteSpace(xmlCountry.CountryName, nameof(xmlCountry.CountryName));
            ArgumentException.ThrowIfNullOrWhiteSpace(xmlCountry.CountryCode, nameof(xmlCountry.CountryCode));
            ArgumentException.ThrowIfNullOrWhiteSpace(xmlCountry.DateFormat, nameof(xmlCountry.DateFormat));
            ArgumentException.ThrowIfNullOrWhiteSpace(xmlCountry.TimeFormat, nameof(xmlCountry.TimeFormat));

            var country = await this.countryRepository.GetByHattrickIdAsync(xmlCountry.CountryId.Value);

            country ??= await this.countryRepository.InsertAsync(
                new Domain.Country
                {
                    HattrickId = xmlCountry.CountryId.Value,
                    Name = xmlCountry.CountryName,
                    Code = xmlCountry.CountryCode,
                    DateFormat = xmlCountry.DateFormat,
                    TimeFormat = xmlCountry.TimeFormat,
                    Currency = currency,
                    League = league
                });

            return country;
        }

        private async Task<Domain.Currency> ProcessCurrencyAsync(
            Models.WorldDetails.Country xmlCountry,
            CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(xmlCountry.CurrencyName, nameof(xmlCountry.CurrencyName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyRate, nameof(xmlCountry.CurrencyRate));

            var currency = await this.currencyRepository.Query(x => x.Name == xmlCountry.CurrencyName
                                                                 && x.Rate == xmlCountry.CurrencyRate)
                .SingleOrDefaultAsync(cancellationToken);

            if (currency == null)
            {
                currency = await this.currencyRepository.InsertAsync(
                    new Domain.Currency
                    {
                        Name = xmlCountry.CurrencyName,
                        Rate = xmlCountry.CurrencyRate.Value
                    });

                // Force identity generation.
                await this.databaseContext.SaveAsync();
            }

            return currency;
        }

        private async Task<Domain.League> ProcessLeagueAsync(
            Models.WorldDetails.League xmlLeague,
            CancellationToken cancellationToken)
        {
            var league = await this.leagueRepository.GetByHattrickIdAsync(xmlLeague.LeagueId);

            var flagBytes = await GetImageBytesFromCacheAsync(
                string.Format(
                    LeagueFlagImageUrlMask,
                    xmlLeague.LeagueId.ToString()),
                cancellationToken);

            if (league == null)
            {
                league = await this.leagueRepository.InsertAsync(
                    new Domain.League
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
                        SeniorNationalTeamHattrickId = xmlLeague.NationalTeamId,
                        JuniorNationalTeamHattrickId = xmlLeague.U20TeamId,
                        ActiveTeams = xmlLeague.ActiveTeams,
                        ActiveUsers = xmlLeague.ActiveUsers,
                        WaitingUsers = xmlLeague.WaitingUsers,
                        LeagueLevels = xmlLeague.NumberOfLevels,
                        FlagBytes = flagBytes
                    });
            }
            else
            {
                league.Season = xmlLeague.Season;
                league.Week = xmlLeague.MatchRound;
                league.ActiveTeams = xmlLeague.ActiveTeams;
                league.ActiveUsers = xmlLeague.ActiveUsers;
                league.WaitingUsers = xmlLeague.WaitingUsers;
                league.LeagueLevels = xmlLeague.NumberOfLevels;
                league.SeniorNationalTeamHattrickId = xmlLeague.NationalTeamId;
                league.JuniorNationalTeamHattrickId = xmlLeague.U20TeamId;

                this.leagueRepository.Update(league);
            }

            return league;
        }

        private async Task ProcessLeagueCupAsync(
            Models.WorldDetails.Cup xmlCup,
            Domain.League league,
            CancellationToken cancellationToken)
        {
            var leagueCup = await this.leagueCupRepository.GetByHattrickIdAsync(xmlCup.CupId);

            if (leagueCup == null)
            {
                await this.leagueCupRepository.InsertAsync(
                    new Domain.LeagueCup
                    {
                        HattrickId = xmlCup.CupId,
                        Name = xmlCup.CupName,
                        Type = (LeagueCupType)xmlCup.CupLevel,
                        SubType = (LeagueCupSubType)xmlCup.CupLevelIndex,
                        Level = xmlCup.CupLeagueLevel,
                        Week = xmlCup.MatchRound,
                        WeeksLeft = xmlCup.MatchRoundsLeft,
                        League = league
                    });
            }
            else
            {
                leagueCup.Name = xmlCup.CupName;
                leagueCup.Week = xmlCup.MatchRound;
                leagueCup.WeeksLeft = xmlCup.MatchRoundsLeft;

                this.leagueCupRepository.Update(leagueCup);
            }
        }

        private async Task ProcessLeagueScheduleAsync(
            Models.WorldDetails.League xmlLeague,
            Domain.League league,
            CancellationToken cancellationToken)
        {
            var leagueSchedule = await this.leagueScheduleRepository.Query(x => x.LeagueHattrickId == league.HattrickId)
                .SingleOrDefaultAsync(cancellationToken);

            if (leagueSchedule == null)
            {
                leagueSchedule = await this.leagueScheduleRepository.InsertAsync(
                    new Domain.LeagueSchedule
                    {
                        NextTrainingUpdate = xmlLeague.TrainingDate,
                        NextEconomyUpdate = xmlLeague.EconomyDate,
                        NextSeriesMatchDate = xmlLeague.SeriesMatchDate,
                        NextCupMatchDate = xmlLeague.CupMatchDate,
                        FirstDailyUpdate = xmlLeague.Sequence1,
                        SecondDailyUpdate = xmlLeague.Sequence2,
                        ThirdDailyUpdate = xmlLeague.Sequence3,
                        FourthDailyUpdate = xmlLeague.Sequence5,
                        FifthDailyUpdate = xmlLeague.Sequence7,
                        League = league
                    });
            }
            else
            {
                leagueSchedule.NextTrainingUpdate = xmlLeague.TrainingDate;
                leagueSchedule.NextEconomyUpdate = xmlLeague.EconomyDate;
                leagueSchedule.NextSeriesMatchDate = xmlLeague.SeriesMatchDate;
                leagueSchedule.NextCupMatchDate = xmlLeague.CupMatchDate;
                leagueSchedule.FirstDailyUpdate = xmlLeague.Sequence1;
                leagueSchedule.SecondDailyUpdate = xmlLeague.Sequence2;
                leagueSchedule.ThirdDailyUpdate = xmlLeague.Sequence3;
                leagueSchedule.FourthDailyUpdate = xmlLeague.Sequence5;
                leagueSchedule.FifthDailyUpdate = xmlLeague.Sequence7;

                this.leagueScheduleRepository.Update(leagueSchedule);
            }
        }

        private async Task ProcessRegionAsync(
            Models.IdName xmlRegion,
            Domain.Country country,
            CancellationToken cancellationToken)
        {
            var region = await this.regionRepository.GetByHattrickIdAsync(xmlRegion.Id);

            if (region == null)
            {
                await this.regionRepository.InsertAsync(
                    new Domain.Region
                    {
                        HattrickId = xmlRegion.Id,
                        Name = xmlRegion.Name,
                        Country = country
                    });
            }
        }
    }
}