namespace Hyperar.HPA.Domain.Junior
{
    using System.Collections.Generic;
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchTeamLineUp : AuditableEntityBase, IAuditableEntity
    {
        public SkillLevel Experience { get; set; }

        public long MatchHattrickId { get; set; }

        public virtual MatchTeam MatchTeam { get; set; } = new MatchTeam();

        public virtual ICollection<MatchTeamLineUpPlayer> Players { get; set; } = new HashSet<MatchTeamLineUpPlayer>();

        public int PlayStyle { get; set; }

        public virtual ICollection<MatchTeamLineUpStartingPlayer> StartingPlayers { get; set; } = new HashSet<MatchTeamLineUpStartingPlayer>();

        public virtual ICollection<MatchTeamLineUpSubstitution> Substitutions { get; set; } = new HashSet<MatchTeamLineUpSubstitution>();

        public long TeamHattrickId { get; set; }
    }
}