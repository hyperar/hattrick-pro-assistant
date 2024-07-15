namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    using System.Collections.Generic;

    public class Team
    {
        public int ExperienceLevel { get; set; }

        public List<Player> LineUp { get; set; } = new List<Player>();

        public List<StartingPlayer> StartingLineUp { get; set; } = new List<StartingPlayer>();

        public int StyleOfPlay { get; set; }

        public List<Substitution> Substitutions { get; set; } = new List<Substitution>();

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}