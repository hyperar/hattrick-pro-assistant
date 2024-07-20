namespace Hyperar.HPA.Domain.Senior
{
    using System.Collections.Generic;
    using Domain.Interfaces;
    using Shared.Enums;

    public class MatchTeam : AuditableEntityBase, IAuditableEntity
    {
        public MatchSectorRating AttackIndirectSetPiecesRating { get; set; }

        public MatchTeamAttitude? Attitude { get; set; }

        public virtual ICollection<MatchTeamBooking> Bookings { get; set; } = new HashSet<MatchTeamBooking>();

        public MatchSectorRating CenterAttackRating { get; set; }

        public MatchSectorRating CenterDefenseRating { get; set; }

        public int ChancesOnCenter { get; set; }

        public int ChancesOnLeft { get; set; }

        public int ChancesOnRight { get; set; }

        public MatchSectorRating DefenseIndirectSetPiecesRating { get; set; }

        public int FirstHalfPossession { get; set; }

        public string Formation { get; set; } = string.Empty;

        public virtual ICollection<MatchTeamGoal> Goals { get; set; } = new HashSet<MatchTeamGoal>();

        public virtual ICollection<MatchTeamInjury> Injuries { get; set; } = new HashSet<MatchTeamInjury>();

        public MatchSectorRating LeftAttackRating { get; set; }

        public MatchSectorRating LeftDefenseRating { get; set; }

        public virtual MatchTeamLineUp? LineUp { get; set; }

        public MatchTeamLocation Location { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public long MatchHattrickId { get; set; }

        public byte[] MatchKitBytes { get; set; } = Array.Empty<byte>();

        public MatchSectorRating MidfieldRating { get; set; }

        public string Name { get; set; } = string.Empty;

        public int OtherChances { get; set; }

        public MatchSectorRating RightAttackRating { get; set; }

        public MatchSectorRating RightDefenseRating { get; set; }

        public int SecondHalfPossession { get; set; }

        public int SpecialEventChances { get; set; }

        public SkillLevel TacticSkill { get; set; }

        public MatchTacticType TacticType { get; set; }

        public long TeamHattrickId { get; set; }
    }
}