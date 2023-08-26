namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{

    public class Team
    {
        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public Arena Arena { get; set; } = new Arena();

        public League League { get; set; } = new League();

        public Country Country { get; set; } = new Country();

        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        public Region Region { get; set; } = new Region();

        public YouthTeam? YouthTeam { get; set; } = null;

    }
}
