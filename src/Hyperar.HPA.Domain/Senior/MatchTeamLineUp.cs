namespace Hyperar.HPA.Domain.Senior
{
    using System.Collections.Generic;
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchLineUp;

    public class MatchTeamLineUp : EntityBase, IEntity
    {
        public MatchTeamLineUp()
        {
            this.MatchTeam = new MatchTeam();
            this.Players = new HashSet<MatchTeamLineUpPlayer>();
            this.StartingPlayers = new HashSet<MatchTeamLineUpStartingPlayer>();
            this.Substitutions = new HashSet<MatchTeamLineUpSubstitution>();
        }

        public SkillLevel Experience { get; set; }

        public virtual MatchTeam MatchTeam { get; set; }

        public int MatchTeamId { get; set; }

        public virtual ICollection<MatchTeamLineUpPlayer> Players { get; set; }

        public virtual ICollection<MatchTeamLineUpStartingPlayer> StartingPlayers { get; set; }

        public int Style { get; set; }

        public virtual ICollection<MatchTeamLineUpSubstitution> Substitutions { get; set; }

        public static MatchTeamLineUp Create(Models.Team xmlTeam, MatchTeam matchTeam)
        {
            return new MatchTeamLineUp
            {
                Experience = (SkillLevel)xmlTeam.ExperienceLevel,
                Style = xmlTeam.StyleOfPlay,
                MatchTeam = matchTeam
            };
        }
    }
}