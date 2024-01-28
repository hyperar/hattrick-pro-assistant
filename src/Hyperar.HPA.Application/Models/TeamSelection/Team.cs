namespace Hyperar.HPA.Application.Models.TeamSelection
{
    public class Team
    {
        public Country Country { get; set; } = new Country();

        public uint HattrickId { get; set; }

        public byte[]? Logo { get; set; }

        public string Name { get; set; } = string.Empty;

        public Region Region { get; set; } = new Region();

        public Series Series { get; set; } = new Series();

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}