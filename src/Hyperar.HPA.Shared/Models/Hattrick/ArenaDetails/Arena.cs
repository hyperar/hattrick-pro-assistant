namespace Hyperar.HPA.Shared.Models.Hattrick.ArenaDetails
{
    public class Arena
    {
        public string ArenaFallbackImage { get; set; } = string.Empty;

        public long ArenaId { get; set; }

        public string ArenaImage { get; set; } = string.Empty;

        public string ArenaName { get; set; } = string.Empty;

        public CurrentCapacity CurrentCapacity { get; set; } = new CurrentCapacity();

        public ExpandedCapacity ExpandedCapacity { get; set; } = new ExpandedCapacity();

        public IdName League { get; set; } = new IdName();

        public IdName Region { get; set; } = new IdName();

        public IdName Team { get; set; } = new IdName();
    }
}