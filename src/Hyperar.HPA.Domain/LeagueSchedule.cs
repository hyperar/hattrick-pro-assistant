namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;

    public class LeagueSchedule : EntityBase, IEntity
    {
        public DateTime FifthDailyUpdate { get; set; }

        public DateTime FirstDailyUpdate { get; set; }

        public DateTime FourthDailyUpdate { get; set; }

        public virtual League League { get; set; } = new League();

        public long LeagueHattrickId { get; set; }

        public DateTime? NextCupMatchDate { get; set; }

        public DateTime NextEconomyUpdate { get; set; }

        public DateTime? NextSeriesMatchDate { get; set; }

        public DateTime NextTrainingUpdate { get; set; }

        public DateTime SecondDailyUpdate { get; set; }

        public DateTime ThirdDailyUpdate { get; set; }
    }
}