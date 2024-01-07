namespace Hyperar.HPA.Application.Models.TeamSelection
{
    public class SeniorTeam
    {
        public Country Country { get; set; } = new Country();

        public uint HattrickId { get; set; }

        public byte[]? Logo { get; set; }

        public string Name { get; set; } = string.Empty;

        public Region Region { get; set; } = new Region();

        public SeniorSeries SeniorSeries { get; set; } = new SeniorSeries();

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}