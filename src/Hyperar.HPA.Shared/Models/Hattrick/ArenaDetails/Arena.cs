namespace Hyperar.HPA.Shared.Models.Hattrick.ArenaDetails
{
    public class Arena
    {
        public Arena()
        {
            this.CurrentCapacity = new CurrentCapacity();
            this.ExpandedCapacity = new ExpandedCapacity();
            this.League = new IdName();
            this.Region = new IdName();
            this.Team = new IdName();

            this.ArenaFallbackImage = string.Empty;
            this.ArenaImage = string.Empty;
            this.ArenaName = string.Empty;
        }

        public string ArenaFallbackImage { get; set; }

        public long ArenaId { get; set; }

        public string ArenaImage { get; set; }

        public string ArenaName { get; set; }

        public CurrentCapacity CurrentCapacity { get; set; }

        public ExpandedCapacity ExpandedCapacity { get; set; }

        public IdName League { get; set; }

        public IdName Region { get; set; }

        public IdName Team { get; set; }
    }
}