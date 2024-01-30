namespace Hyperar.HPA.Application.Models.Home
{
    public class Team
    {
        public Country Country { get; set; } = new Country();

        public uint HattrickId { get; set; }

        public byte[]? Logo { get; set; }

        public string Name { get; set; } = string.Empty;

        public PlayedMatch[] PlayedMatches { get; set; } = Array.Empty<PlayedMatch>();

        public Player[] Players { get; set; } = Array.Empty<Player>();

        public Region Region { get; set; } = new Region();

        public Series Series { get; set; } = new Series();

        public UpcomingMatch[] UpcomingMatches { get; set; } = Array.Empty<UpcomingMatch>();

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}