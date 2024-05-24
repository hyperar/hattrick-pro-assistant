namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;
    using Senior;
    using Models = Shared.Models.Hattrick.WorldDetails;

    public class Country : HattrickEntityBase, IHattrickEntity
    {
        public Country()
        {
            this.HallOfFamePlayers = new HashSet<HallOfFamePlayer>();
            this.League = new League();
            this.Managers = new HashSet<Manager>();
            this.MatchOfficials = new HashSet<MatchOfficial>();
            this.Players = new HashSet<Player>();
            this.Regions = new HashSet<Region>();

            this.Code = string.Empty;
            this.CurrencyName = string.Empty;
            this.DateFormat = string.Empty;
            this.Name = string.Empty;
            this.TimeFormat = string.Empty;
        }

        public string Code { get; set; }

        public string CurrencyName { get; set; }

        public decimal CurrencyRate { get; set; }

        public string DateFormat { get; set; }

        public virtual ICollection<HallOfFamePlayer> HallOfFamePlayers { get; set; }

        public virtual League League { get; set; }

        public long LeagueHattrickId { get; set; }

        public virtual ICollection<Manager> Managers { get; set; }

        public virtual ICollection<MatchOfficial> MatchOfficials { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Player>? Players { get; set; }

        public virtual ICollection<Region> Regions { get; set; }

        public string TimeFormat { get; set; }

        public static Country Create(Models.Country xmlCountry, Domain.League league)
        {
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryCode, nameof(xmlCountry.CountryCode));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryId, nameof(xmlCountry.CountryId));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryName, nameof(xmlCountry.CountryName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyName, nameof(xmlCountry.CurrencyName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyRate, nameof(xmlCountry.CurrencyRate));
            ArgumentNullException.ThrowIfNull(xmlCountry.DateFormat, nameof(xmlCountry.DateFormat));
            ArgumentNullException.ThrowIfNull(xmlCountry.TimeFormat, nameof(xmlCountry.TimeFormat));

            return new Country
            {
                HattrickId = xmlCountry.CountryId.Value,
                Name = xmlCountry.CountryName,
                CurrencyName = xmlCountry.CurrencyName,
                CurrencyRate = xmlCountry.CurrencyRate.Value,
                Code = xmlCountry.CountryCode,
                DateFormat = xmlCountry.DateFormat,
                League = league,
                TimeFormat = xmlCountry.TimeFormat
            };
        }

        public bool HasChanged(Models.Country xmlCountry)
        {
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryCode, nameof(xmlCountry.CountryCode));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryId, nameof(xmlCountry.CountryId));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryName, nameof(xmlCountry.CountryName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyName, nameof(xmlCountry.CurrencyName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyRate, nameof(xmlCountry.CurrencyRate));
            ArgumentNullException.ThrowIfNull(xmlCountry.DateFormat, nameof(xmlCountry.DateFormat));
            ArgumentNullException.ThrowIfNull(xmlCountry.TimeFormat, nameof(xmlCountry.TimeFormat));

            return this.Name != xmlCountry.CountryName
                || this.CurrencyName != xmlCountry.CurrencyName
                || this.CurrencyRate != xmlCountry.CurrencyRate.Value
                || this.Code != xmlCountry.CountryCode
                || this.DateFormat != xmlCountry.DateFormat
                || this.TimeFormat != xmlCountry.TimeFormat;
        }

        public void Update(Models.Country xmlCountry)
        {
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryCode, nameof(xmlCountry.CountryCode));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryId, nameof(xmlCountry.CountryId));
            ArgumentNullException.ThrowIfNull(xmlCountry.CountryName, nameof(xmlCountry.CountryName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyName, nameof(xmlCountry.CurrencyName));
            ArgumentNullException.ThrowIfNull(xmlCountry.CurrencyRate, nameof(xmlCountry.CurrencyRate));
            ArgumentNullException.ThrowIfNull(xmlCountry.DateFormat, nameof(xmlCountry.DateFormat));
            ArgumentNullException.ThrowIfNull(xmlCountry.TimeFormat, nameof(xmlCountry.TimeFormat));

            this.Name = xmlCountry.CountryName;
            this.CurrencyName = xmlCountry.CurrencyName;
            this.CurrencyRate = xmlCountry.CurrencyRate.Value;
            this.Code = xmlCountry.CountryCode;
            this.DateFormat = xmlCountry.DateFormat;
            this.TimeFormat = xmlCountry.TimeFormat;
        }
    }
}