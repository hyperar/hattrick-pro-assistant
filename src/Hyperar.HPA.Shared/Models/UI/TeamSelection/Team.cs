namespace Hyperar.HPA.Shared.Models.UI.TeamSelection
{
    public class Team
    {
        public Team()
        {
            this.Name = string.Empty;
            this.HomeMatchKitBytes = Array.Empty<byte>();
            this.AwayMatchKitBytes = Array.Empty<byte>();

            this.League = new League();
            this.Country = new Country();
            this.Region = new Region();
            this.Series = new Series();
        }

        public bool IsSelected { get; set; }

        public byte[] AwayMatchKitBytes { get; set; }

        public Country Country { get; set; }

        public long HattrickId { get; set; }

        public byte[] HomeMatchKitBytes { get; set; }

        public League League { get; set; }

        public byte[]? LogoBytes { get; set; }

        public string Name { get; set; }

        public Region Region { get; set; }

        public Series Series { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}