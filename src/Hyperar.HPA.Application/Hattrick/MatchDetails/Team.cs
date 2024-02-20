namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using Common.Enums;

    public class Team
    {
        public string? DressUri { get; set; } = string.Empty;

        public string? Formation { get; set; } = string.Empty;

        public uint? Goals { get; set; }

        public uint? NrOfChancesCenter { get; set; }

        public uint? NrOfChancesLeft { get; set; }

        public uint? NrOfChancesOther { get; set; }

        public uint? NrOfChancesRight { get; set; }

        public uint? NrOfChancesSpecialEvents { get; set; }

        public MatchSectorRating? RatingLeftAtt { get; set; }

        public MatchSectorRating? RatingLeftDef { get; set; }

        public MatchSectorRating? RatingMidAtt { get; set; }

        public MatchSectorRating? RatingMidDef { get; set; }

        public MatchSectorRating? RatingMidfield { get; set; }

        public MatchSectorRating? RatingRightAtt { get; set; }

        public MatchSectorRating? RatingRightDef { get; set; }

        public MatchSectorRating? RatingSetPiecesAtt { get; set; }

        public MatchSectorRating? RatingSetPiecesDef { get; set; }

        public SkillLevel? TacticSkill { get; set; }

        public MatchTacticType? TacticType { get; set; }

        public MatchTeamAttitude? TeamAttitude { get; set; }

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}