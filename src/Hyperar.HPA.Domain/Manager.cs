﻿namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain.Interfaces;

    public class Manager : HattrickEntityBase, IHattrickEntity
    {
        public virtual Country Country { get; set; } = new Country();

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public virtual List<SeniorTeam>? SeniorTeams { get; set; }

        public SupporterTier SupporterTier { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}