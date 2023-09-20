namespace Hyperar.HPA.Domain
{
    using Hyperar.HPA.Domain.Interfaces;

    public class Region : HattrickEntityBase, IHattrickEntity
    {
        public virtual Country? Country { get; set; }

        public uint CountryHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual List<SeniorTeam>? SeniorTeams { get; set; }
    }
}