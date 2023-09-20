namespace Hyperar.HPA.Application.Hattrick.ManagerCompendium
{
    public class Team
    {
        public Arena Arena { get; set; } = new Arena();

        public Country Country { get; set; } = new Country();

        public League League { get; set; } = new League();

        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        public Region Region { get; set; } = new Region();

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public YouthTeam? YouthTeam { get; set; } = null;
    }
}