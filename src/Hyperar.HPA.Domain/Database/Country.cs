namespace Hyperar.HPA.Domain.Database
{
    using System.Collections.Generic;
    using Hyperar.HPA.Domain.Interfaces;

    public class Country : HattrickEntityBase, IEntity, IHattrickEntity
    {
        public string Name { get; set; } = string.Empty;

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public string Code { get; set; } = string.Empty;

        public string DateFormat { get; set; } = string.Empty;

        public string TimeFormat { get; set; } = string.Empty;

        public int LeagueId { get; set; }

        public virtual League? League { get; set; }

        public virtual List<Region>? Regions { get; set; }
    }
}
