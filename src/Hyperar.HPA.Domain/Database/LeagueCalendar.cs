namespace Hyperar.HPA.Domain.Database
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public class LeagueCalendar : EntityBase, IEntity
    {
        public DateTime NextTrainingUpdate { get; set; }

        public DateTime NextEconomyUpdate { get; set; }

        public DateTime NextCupMatchDate { get; set; }

        public DateTime NextSeriesMatchDate { get; set; }

        public DateTime FirstWeeklyUpdate { get; set; }

        public DateTime SecondWeeklyUpdate { get; set; }

        public DateTime ThirdWeeklyUpdate { get; set; }

        public DateTime FourthWeeklyUpdate { get; set; }

        public DateTime FifthWeeklyUpdate { get; set; }

        public int LeagueId { get; set; }

        public virtual League? League { get; set; }
    }
}
