namespace Hyperar.HPA.Domain
{
    using System.Collections.Generic;
    using Domain.Interfaces;

    public class Country : HattrickEntityBase, IHattrickEntity
    {
        public string Code { get; set; } = string.Empty;

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public string DateFormat { get; set; } = string.Empty;

        public virtual League League { get; set; } = new League();

        public uint LeagueHattrickId { get; set; }

        public virtual ICollection<Manager> Managers { get; set; } = new HashSet<Manager>();

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Region> Regions { get; set; } = new HashSet<Region>();

        public virtual ICollection<Player>? Players { get; set; } = new HashSet<Player>();

        public string TimeFormat { get; set; } = string.Empty;
    }
}