namespace Hyperar.HPA.Domain.Junior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class PlayerMatch : AuditableEntityBase, IAuditableEntity
    {
        public decimal AverageRating { get; set; }

        public DateTime Date { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public long MatchHattrickId { get; set; }

        public virtual Player Player { get; set; } = new Player();

        public long PlayerHattrickId { get; set; }

        public MatchRole Role { get; set; }
    }
}