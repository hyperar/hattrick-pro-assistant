namespace Hyperar.HPA.Shared.Models.Hattrick.MatchDetails
{
    public class Team
    {
        public Team()
        {
            this.DressUri = string.Empty;
            this.Formation = string.Empty;
            this.TeamName = string.Empty;
        }

        public string? DressUri { get; set; }

        public string? Formation { get; set; }

        public byte? Goals { get; set; }

        public byte? NrOfChancesCenter { get; set; }

        public byte? NrOfChancesLeft { get; set; }

        public byte? NrOfChancesOther { get; set; }

        public byte? NrOfChancesRight { get; set; }

        public byte? NrOfChancesSpecialEvents { get; set; }

        public byte? RatingLeftAtt { get; set; }

        public byte? RatingLeftDef { get; set; }

        public byte? RatingMidAtt { get; set; }

        public byte? RatingMidDef { get; set; }

        public byte? RatingMidfield { get; set; }

        public byte? RatingRightAtt { get; set; }

        public byte? RatingRightDef { get; set; }

        public byte? RatingSetPiecesAtt { get; set; }

        public byte? RatingSetPiecesDef { get; set; }

        public byte? TacticSkill { get; set; }

        public byte? TacticType { get; set; }

        public byte? TeamAttitude { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}