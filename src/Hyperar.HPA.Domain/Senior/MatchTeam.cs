namespace Hyperar.HPA.Domain.Senior
{
    using Common.Enums;
    using Domain.Interfaces;

    public class MatchTeam : EntityBase, IEntity
    {
        public MatchSectorRating? AttackIndirectSetPiecesRating { get; set; }

        public MatchTeamAttitude? Attitude { get; set; }

        public virtual ICollection<MatchTeamBooking> Bookings { get; set; } = new HashSet<MatchTeamBooking>();

        public MatchSectorRating? CentralAttackRating { get; set; }

        public MatchSectorRating? CentralDefenseRating { get; set; }

        public uint? ChancesOnCenter { get; set; }

        public uint? ChancesOnLeft { get; set; }

        public uint? ChancesOnRight { get; set; }

        public MatchSectorRating? DefenseIndirectSetPiecesRating { get; set; }

        public uint? FirstHalfPosession { get; set; }

        public string? Formation { get; set; } = string.Empty;

        public virtual ICollection<MatchTeamGoal> Goals { get; set; } = new HashSet<MatchTeamGoal>();

        public uint HattrickId { get; set; }

        public virtual ICollection<MatchTeamInjury> Injuries { get; set; } = new HashSet<MatchTeamInjury>();

        public MatchSectorRating? LeftAttackRating { get; set; }

        public MatchSectorRating? LeftDefenseRating { get; set; }

        public virtual MatchTeamLineUp? LineUp { get; set; }

        public virtual Match Match { get; set; } = new Match();

        public byte[]? MatchKitBytes { get; set; }

        public string? MatchKitUrl { get; set; }

        public MatchSectorRating? MidfieldRating { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint? OtherChances { get; set; }

        public MatchSectorRating? RightAttackRating { get; set; }

        public MatchSectorRating? RightDefenseRating { get; set; }

        public uint? Score { get; set; }

        public uint? SecondHalfPosession { get; set; }

        public uint? SpecialEventChances { get; set; }

        public SkillLevel? TacticLevel { get; set; }

        public MatchTacticType? TacticType { get; set; }
    }
}