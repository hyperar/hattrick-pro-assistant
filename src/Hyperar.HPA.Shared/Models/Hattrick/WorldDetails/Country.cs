namespace Hyperar.HPA.Shared.Models.Hattrick.WorldDetails
{
    using System.Collections.Generic;

    public class Country
    {
        public Country()
        {
            this.RegionList = new List<IdName>();
        }

        public bool Available { get; set; }

        public string? CountryCode { get; set; }

        public long? CountryId { get; set; }

        public string? CountryName { get; set; }

        public string? CurrencyName { get; set; }

        public decimal? CurrencyRate { get; set; }

        public string? DateFormat { get; set; }

        public List<IdName> RegionList { get; set; }

        public string? TimeFormat { get; set; }
    }
}