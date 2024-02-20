namespace Hyperar.HPA.Application.Hattrick.MatchLineUp
{
    using System.Collections.Generic;
    using Common.Enums;

    public class Team
    {
        public SkillLevel ExperienceLevel { get; set; }

        public List<Player> LineUp { get; set; } = new List<Player>();

        public List<StartingPlayer> StartingLineUp { get; set; } = new List<StartingPlayer>();

        public int StyleOfPlay { get; set; }

        public List<Substitution> Substitutions { get; set; } = new List<Substitution>();

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}