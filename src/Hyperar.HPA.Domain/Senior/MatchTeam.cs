namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class MatchTeam : EntityBase, IEntity
    {
        public MatchTeam()
        {
            this.Bookings = new HashSet<MatchTeamBooking>();
            this.Goals = new HashSet<MatchTeamGoal>();
            this.Injuries = new HashSet<MatchTeamInjury>();
            this.Match = new Match();

            this.Name = string.Empty;
        }

        public MatchSectorRating? AttackIndirectSetPiecesRating { get; set; }

        public MatchTeamAttitude? Attitude { get; set; }

        public virtual ICollection<MatchTeamBooking> Bookings { get; set; }

        public MatchSectorRating? CentralAttackRating { get; set; }

        public MatchSectorRating? CentralDefenseRating { get; set; }

        public byte? ChancesOnCenter { get; set; }

        public byte? ChancesOnLeft { get; set; }

        public byte? ChancesOnRight { get; set; }

        public MatchSectorRating? DefenseIndirectSetPiecesRating { get; set; }

        public byte? FirstHalfPosession { get; set; }

        public string? Formation { get; set; }

        public virtual ICollection<MatchTeamGoal> Goals { get; set; }

        public long HattrickId { get; set; }

        public virtual ICollection<MatchTeamInjury> Injuries { get; set; }

        public MatchSectorRating? LeftAttackRating { get; set; }

        public MatchSectorRating? LeftDefenseRating { get; set; }

        public virtual MatchTeamLineUp? LineUp { get; set; }

        public virtual Match Match { get; set; }

        public long MatchHattrickId { get; set; }

        public byte[]? MatchKitBytes { get; set; }

        public string? MatchKitUrl { get; set; }

        public MatchSectorRating? MidfieldRating { get; set; }

        public string Name { get; set; }

        public byte? OtherChances { get; set; }

        public MatchSectorRating? RightAttackRating { get; set; }

        public MatchSectorRating? RightDefenseRating { get; set; }

        public byte? Score { get; set; }

        public byte? SecondHalfPosession { get; set; }

        public byte? SpecialEventChances { get; set; }

        public SkillLevel? TacticLevel { get; set; }

        public MatchTacticType? TacticType { get; set; }

        public static MatchTeam Create(Models.Team xmlTeam, byte? firstHalfPosession, byte? secondHalfPosession, Match match)
        {
            return new MatchTeam
            {
                AttackIndirectSetPiecesRating = xmlTeam.RatingSetPiecesAtt != null ? (MatchSectorRating)xmlTeam.RatingSetPiecesAtt : null,
                Attitude = xmlTeam.TeamAttitude != null ? (MatchTeamAttitude)xmlTeam.TeamAttitude : null,
                CentralAttackRating = xmlTeam.RatingMidAtt != null ? (MatchSectorRating)xmlTeam.RatingMidAtt : null,
                CentralDefenseRating = xmlTeam.RatingMidDef != null ? (MatchSectorRating)xmlTeam.RatingMidDef : null,
                ChancesOnCenter = xmlTeam.NrOfChancesCenter,
                ChancesOnLeft = xmlTeam.NrOfChancesLeft,
                ChancesOnRight = xmlTeam.NrOfChancesRight,
                DefenseIndirectSetPiecesRating = xmlTeam.RatingSetPiecesDef != null ? (MatchSectorRating)xmlTeam.RatingSetPiecesDef : null,
                FirstHalfPosession = firstHalfPosession,
                Formation = xmlTeam.Formation,
                HattrickId = xmlTeam.TeamId,
                LeftAttackRating = xmlTeam.RatingLeftAtt != null ? (MatchSectorRating)xmlTeam.RatingLeftAtt : null,
                LeftDefenseRating = xmlTeam.RatingLeftDef != null ? (MatchSectorRating)xmlTeam.RatingLeftDef : null,
                Match = match,
                MidfieldRating = xmlTeam.RatingMidfield != null ? (MatchSectorRating)xmlTeam.RatingMidfield : null,
                Name = xmlTeam.TeamName,
                OtherChances = xmlTeam.NrOfChancesOther,
                RightAttackRating = xmlTeam.RatingRightAtt != null ? (MatchSectorRating)xmlTeam.RatingRightAtt : null,
                RightDefenseRating = xmlTeam.RatingRightDef != null ? (MatchSectorRating)xmlTeam.RatingRightDef : null,
                Score = xmlTeam.Goals,
                SecondHalfPosession = secondHalfPosession,
                SpecialEventChances = xmlTeam.NrOfChancesSpecialEvents,
                TacticLevel = xmlTeam.TacticSkill != null ? (SkillLevel)xmlTeam.TacticSkill : null,
                TacticType = xmlTeam.TacticType != null ? (MatchTacticType)xmlTeam.TacticType : null
            };
        }

        public bool HasChanged(Models.Team xmlTeam, byte? firstHalfPosession, byte? secondHalfPosession)
        {
            return this.AttackIndirectSetPiecesRating != (xmlTeam.RatingSetPiecesAtt != null ? (MatchSectorRating)xmlTeam.RatingSetPiecesAtt : null)
                || this.Attitude != (xmlTeam.TeamAttitude != null ? (MatchTeamAttitude)xmlTeam.TeamAttitude : null)
                || this.CentralAttackRating != (xmlTeam.RatingMidAtt != null ? (MatchSectorRating)xmlTeam.RatingMidAtt : null)
                || this.CentralDefenseRating != (xmlTeam.RatingMidDef != null ? (MatchSectorRating)xmlTeam.RatingMidDef : null)
                || this.ChancesOnCenter != xmlTeam.NrOfChancesCenter
                || this.ChancesOnLeft != xmlTeam.NrOfChancesLeft
                || this.ChancesOnRight != xmlTeam.NrOfChancesRight
                || this.DefenseIndirectSetPiecesRating != (xmlTeam.RatingSetPiecesDef != null ? (MatchSectorRating)xmlTeam.RatingSetPiecesDef : null)
                || this.FirstHalfPosession != firstHalfPosession
                || this.Formation != xmlTeam.Formation
                || this.LeftAttackRating != (xmlTeam.RatingLeftAtt != null ? (MatchSectorRating)xmlTeam.RatingLeftAtt : null)
                || this.LeftDefenseRating != (xmlTeam.RatingLeftDef != null ? (MatchSectorRating)xmlTeam.RatingLeftDef : null)
                || this.MidfieldRating != (xmlTeam.RatingMidfield != null ? (MatchSectorRating)xmlTeam.RatingMidfield : null)
                || this.Name != xmlTeam.TeamName
                || this.OtherChances != xmlTeam.NrOfChancesOther
                || this.RightAttackRating != (xmlTeam.RatingRightAtt != null ? (MatchSectorRating)xmlTeam.RatingRightAtt : null)
                || this.RightDefenseRating != (xmlTeam.RatingRightDef != null ? (MatchSectorRating)xmlTeam.RatingRightDef : null)
                || this.Score != xmlTeam.Goals
                || this.SecondHalfPosession != secondHalfPosession
                || this.SpecialEventChances != xmlTeam.NrOfChancesSpecialEvents
                || this.TacticLevel != (xmlTeam.TacticSkill != null ? (SkillLevel)xmlTeam.TacticSkill : null)
                || this.TacticType != (xmlTeam.TacticType != null ? (MatchTacticType)xmlTeam.TacticType : null);
        }

        public void Update(Models.Team xmlTeam, byte? firstHalfPosession, byte? secondHalfPosession)
        {
            this.AttackIndirectSetPiecesRating = xmlTeam.RatingSetPiecesAtt != null ? (MatchSectorRating)xmlTeam.RatingSetPiecesAtt : null;
            this.Attitude = xmlTeam.TeamAttitude != null ? (MatchTeamAttitude)xmlTeam.TeamAttitude : null;
            this.CentralAttackRating = xmlTeam.RatingMidAtt != null ? (MatchSectorRating)xmlTeam.RatingMidAtt : null;
            this.CentralDefenseRating = xmlTeam.RatingMidDef != null ? (MatchSectorRating)xmlTeam.RatingMidDef : null;
            this.ChancesOnCenter = xmlTeam.NrOfChancesCenter;
            this.ChancesOnLeft = xmlTeam.NrOfChancesLeft;
            this.ChancesOnRight = xmlTeam.NrOfChancesRight;
            this.DefenseIndirectSetPiecesRating = xmlTeam.RatingSetPiecesDef != null ? (MatchSectorRating)xmlTeam.RatingSetPiecesDef : null;
            this.FirstHalfPosession = firstHalfPosession;
            this.Formation = xmlTeam.Formation;
            this.LeftAttackRating = xmlTeam.RatingLeftAtt != null ? (MatchSectorRating)xmlTeam.RatingLeftAtt : null;
            this.LeftDefenseRating = xmlTeam.RatingLeftDef != null ? (MatchSectorRating)xmlTeam.RatingLeftDef : null;
            this.MidfieldRating = xmlTeam.RatingMidfield != null ? (MatchSectorRating)xmlTeam.RatingMidfield : null;
            this.Name = xmlTeam.TeamName;
            this.OtherChances = xmlTeam.NrOfChancesOther;
            this.RightAttackRating = xmlTeam.RatingRightAtt != null ? (MatchSectorRating)xmlTeam.RatingRightAtt : null;
            this.RightDefenseRating = xmlTeam.RatingRightDef != null ? (MatchSectorRating)xmlTeam.RatingRightDef : null;
            this.Score = xmlTeam.Goals;
            this.SecondHalfPosession = secondHalfPosession;
            this.SpecialEventChances = xmlTeam.NrOfChancesSpecialEvents;
            this.TacticLevel = xmlTeam.TacticSkill != null ? (SkillLevel)xmlTeam.TacticSkill : null;
            this.TacticType = xmlTeam.TacticType != null ? (MatchTacticType)xmlTeam.TacticType : null;
        }
    }
}