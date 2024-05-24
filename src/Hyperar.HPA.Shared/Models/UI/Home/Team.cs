namespace Hyperar.HPA.Shared.Models.UI.Home
{
    public class Team
    {
        public Team()
        {
            this.Name = string.Empty;
            this.HomeMatchKitBytes = Array.Empty<byte>();
            this.AwayMatchKitBytes = Array.Empty<byte>();

            this.Manager = new Manager();
            this.League = new League();
            this.Country = new Country();
            this.Region = new Region();
            this.Series = new Series();
            this.UpcomingMatches = new List<Match>();
            this.RecentMatches = new List<RecentMatch>();
            this.Players = new List<Player>();
        }

        public byte[] AwayMatchKitBytes { get; set; }

        public Country Country { get; set; }

        public long HattrickId { get; set; }

        public byte[] HomeMatchKitBytes { get; set; }

        public League League { get; set; }

        public byte[]? LogoBytes { get; set; }

        public Manager Manager { get; set; }

        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }

        public ICollection<RecentMatch> RecentMatches { get; set; }

        public Region Region { get; set; }

        public Series Series { get; set; }

        public ICollection<Match> UpcomingMatches { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}