namespace Hyperar.HPA.Application.Models.Home
{
    public class SeniorTeam
    {
        public Country Country { get; set; } = new Country();

        public uint HattrickId { get; set; }

        public byte[]? Logo { get; set; }

        public string Name { get; set; } = string.Empty;

        public PlayedMatch[] PlayedMatches { get; set; } = Array.Empty<PlayedMatch>();

        public Region Region { get; set; } = new Region();

        public SeniorPlayer[] SeniorPlayers { get; set; } = Array.Empty<SeniorPlayer>();

        public SeniorSeries SeniorSeries { get; set; } = new SeniorSeries();

        public UpcomingMatch[] UpcomingMatches { get; set; } = Array.Empty<UpcomingMatch>();

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}