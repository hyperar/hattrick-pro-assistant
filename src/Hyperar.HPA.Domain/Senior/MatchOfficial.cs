namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;

    public class MatchOfficial : EntityBase, IEntity
    {
        public virtual Country Country { get; set; } = new Country();

        public uint HattrickId { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public string Name { get; set; } = string.Empty;
    }
}