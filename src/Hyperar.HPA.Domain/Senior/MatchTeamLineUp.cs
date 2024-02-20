namespace Hyperar.HPA.Domain.Senior
{
    using System.Collections.Generic;
    using Common.Enums;
    using Domain.Interfaces;

    public class MatchTeamLineUp : EntityBase, IEntity
    {
        public SkillLevel Experience { get; set; }

        public virtual ICollection<MatchTeamLineUpPlayer> Players { get; set; } = new HashSet<MatchTeamLineUpPlayer>();

        public virtual ICollection<MatchTeamLineUpStartingPlayer> StartingPlayers { get; set; } = new HashSet<MatchTeamLineUpStartingPlayer>();

        public int Style { get; set; }

        public virtual ICollection<MatchTeamLineUpSubstitution> Substitutions { get; set; } = new HashSet<MatchTeamLineUpSubstitution>();

        public virtual MatchTeam Team { get; set; } = new MatchTeam();

        public int TeamId { get; set; }
    }
}