namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Team
    {
        public string? DressUri { get; set; }

        public string? Formation { get; set; }

        public int? Goals { get; set; }

        public int? NrOfChancesCenter { get; set; }

        public int? NrOfChancesLeft { get; set; }

        public int? NrOfChancesOther { get; set; }

        public int? NrOfChancesRight { get; set; }

        public int? NrOfChancesSpecialEvents { get; set; }

        public int? RatingLeftAtt { get; set; }

        public int? RatingLeftDef { get; set; }

        public int? RatingMidAtt { get; set; }

        public int? RatingMidDef { get; set; }

        public int? RatingMidfield { get; set; }

        public int? RatingRightAtt { get; set; }

        public int? RatingRightDef { get; set; }

        public int? RatingSetPiecesAtt { get; set; }

        public int? RatingSetPiecesDef { get; set; }

        public int? TacticSkill { get; set; }

        public int? TacticType { get; set; }

        public int? TeamAttitude { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}