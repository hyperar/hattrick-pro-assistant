namespace Hyperar.HPA.Shared.Models.Hattrick.LeagueDetails
{
    public class Team
    {
        public int Draws { get; set; }

        public int GoalsAgainst { get; set; }

        public int GoalsFor { get; set; }

        public int Lost { get; set; }

        public int Matches { get; set; }

        public int Points { get; set; }

        public int Position { get; set; }

        public int PositionChange { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public long UserId { get; set; }

        public int Won { get; set; }
    }
}