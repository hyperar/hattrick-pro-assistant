namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class WorldDetails : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private const string FlagImageUrlMask = "https://www.hattrick.org/Img/flags/{0}.png";

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

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.WorldDetails.HattrickData file)
            {
                try
                {
                    foreach (var xmlLeague in file.LeagueList)
                    {
                        var league = await this.ProcessLeagueAsync(xmlLeague);

                        foreach (var xmlCup in xmlLeague.Cups)
                        {
                            await this.ProcessLeagueCupAsync(xmlCup, league);
                        }

                        var country = await this.ProcessCountryAsync(xmlLeague.Country, league);

                        if (xmlLeague.Country.RegionList.Count > 0)
                        {
                            ArgumentNullException.ThrowIfNull(country, nameof(country));

                            foreach (var xmlRegion in xmlLeague.Country.RegionList)
                            {
                                await this.ProcessRegionAsync(xmlRegion, country);
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.WorldDetails.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task<Domain.Country?> ProcessCountryAsync(Models.WorldDetails.Country xmlCountry, Domain.League league)
        {
            if (!xmlCountry.Available)
            {
                return null;
            }

            ArgumentNullException.ThrowIfNull(xmlCountry.CountryCode, nameof(xmlCountry.CountryCode));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryId, nameof(xmlCountry.CountryId));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryName, nameof(xmlCountry.CountryName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyName, nameof(xmlCountry.CurrencyName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyRate, nameof(xmlCountry.CurrencyRate));
            ArgumentNullException.ThrowIfNull(xmlCountry.DateFormat, nameof(xmlCountry.DateFormat));
            ArgumentNullException.ThrowIfNull(xmlCountry.TimeFormat, nameof(xmlCountry.TimeFormat));

            var country = await this.countryRepository.GetByHattrickIdAsync(xmlCountry.CountryId.Value);

            if (country == null)
            {
                country = Domain.Country.Create(xmlCountry, league);

                await this.countryRepository.InsertAsync(country);
            }
            else if (country.HasChanged(xmlCountry))
            {
                country.Update(xmlCountry);

                this.countryRepository.Update(country);
            }

            return country;
        }

        private async Task<Domain.League> ProcessLeagueAsync(Models.WorldDetails.League xmlLeague)
        {
            var league = await this.leagueRepository.GetByHattrickIdAsync(xmlLeague.LeagueId);

            if (league == null)
            {
                var flagImageBytes = await GetImageBytesFromCacheAsync(
                    string.Format(
                        FlagImageUrlMask,
                        xmlLeague.LeagueId));

                league = Domain.League.Create(
                    xmlLeague,
                    flagImageBytes);

                await this.leagueRepository.InsertAsync(league);
            }
            else if (league.HasChanged(xmlLeague))
            {
                league.Update(xmlLeague);

                this.leagueRepository.Update(league);
            }

            return league;
        }

        private async Task ProcessLeagueCupAsync(Models.WorldDetails.Cup xmlCup, Domain.League league)
        {
            var leagueCup = await this.leagueCupRepository.GetByHattrickIdAsync(xmlCup.CupId);

            if (leagueCup == null)
            {
                leagueCup = Domain.LeagueCup.Create(
                    xmlCup,
                    league);

                await this.leagueCupRepository.InsertAsync(leagueCup);
            }
            else if (leagueCup.HasChanged(xmlCup))
            {
                leagueCup.Update(xmlCup);

                this.leagueCupRepository.Update(leagueCup);
            }
        }

        private async Task ProcessRegionAsync(Models.IdName xmlRegion, Domain.Country country)
        {
            var region = await this.regionRepository.GetByHattrickIdAsync(xmlRegion.Id);

            if (region == null)
            {
                region = Domain.Region.Create(xmlRegion, country);

                await this.regionRepository.InsertAsync(region);
            }
            else if (region.HasChanged(xmlRegion))
            {
                region.Update(xmlRegion);

                this.regionRepository.Update(region);
            }
        }
    }
}