namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;

    public class MatchReferee : AuditableEntityBase, IAuditableEntity
    {
        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public long MatchHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public long RefereeHattrickId { get; set; }
    }
}