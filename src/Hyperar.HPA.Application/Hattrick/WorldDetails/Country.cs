﻿namespace Hyperar.HPA.Application.Hattrick.WorldDetails
{
    using System.Collections.Generic;

    public class Country
    {
        public bool Available { get; set; }

        public string? CountryCode { get; set; }

        public uint? CountryId { get; set; }

        public string? CountryName { get; set; }

        public string? CurrencyName { get; set; }

        public decimal? CurrencyRate { get; set; }

        public string? DateFormat { get; set; }

        public List<Region> RegionList { get; set; } = new List<Region>();

        public string? TimeFormat { get; set; }
    }
}