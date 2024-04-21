namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.LineUp = new List<Player>();
            this.StartingLineUp = new List<StartingPlayer>();
            this.Substitutions = new List<Substitution>();

            this.TeamName = string.Empty;
        }

        public byte ExperienceLevel { get; set; }

        public List<Player> LineUp { get; set; }

        public List<StartingPlayer> StartingLineUp { get; set; }

        public short StyleOfPlay { get; set; }

        public List<Substitution> Substitutions { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}