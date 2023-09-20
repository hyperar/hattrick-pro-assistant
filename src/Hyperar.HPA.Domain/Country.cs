namespace Hyperar.HPA.Domain
{
    using System.Collections.Generic;
    using Hyperar.HPA.Domain.Interfaces;

    public class Country : HattrickEntityBase, IHattrickEntity
    {
        public string Code { get; set; } = string.Empty;

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public string DateFormat { get; set; } = string.Empty;

        public virtual League? League { get; set; }

        public uint LeagueHattrickId { get; set; }

        public virtual List<Manager> Managers { get; set; } = new List<Manager>();

        public string Name { get; set; } = string.Empty;

        public virtual List<Region>? Regions { get; set; }

        public virtual List<SeniorPlayer>? SeniorPlayers { get; set; } = new List<SeniorPlayer>();

        public string TimeFormat { get; set; } = string.Empty;
    }
}