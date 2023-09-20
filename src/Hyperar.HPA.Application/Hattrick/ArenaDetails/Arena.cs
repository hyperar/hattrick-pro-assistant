namespace Hyperar.HPA.Application.Hattrick.ArenaDetails
{
    public class Arena
    {
        public string ArenaFallbackImage { get; set; } = string.Empty;

        public uint ArenaId { get; set; }

        public string ArenaImage { get; set; } = string.Empty;

        public string ArenaName { get; set; } = string.Empty;

        public CurrentCapacity CurrentCapacity { get; set; } = new CurrentCapacity();

        public ExpandedCapacity ExpandedCapacity { get; set; } = new ExpandedCapacity();

        public League League { get; set; } = new League();

        public Region Region { get; set; } = new Region();

        public Team Team { get; set; } = new Team();
    }
}