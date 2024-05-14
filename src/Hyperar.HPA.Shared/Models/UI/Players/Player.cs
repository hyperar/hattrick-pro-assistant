namespace Hyperar.HPA.Shared.Models.UI.Players
{
    using Shared.Enums;

    public class Player
    {
        public Player()
        {
            this.AvatarBytes = Array.Empty<byte>();
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Country = new Country();
        }

        public decimal AgeAux
        {
            get
            {
                return decimal.Parse(
                    $"{this.AgeYears}{Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator}{this.AgeDays:000}");
            }
        }

        public byte AgeDays { get; set; }

        public byte AgeYears { get; set; }

        public AggressivenessLevel AggressivenessLevel { get; set; }

        public AgreeabilityLevel AgreeabilityLevel { get; set; }

        public byte[] AvatarBytes { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public short CareerHattricks { get; set; }

        public short CareerLeagueGoals { get; set; }

        public Country Country { get; set; }

        public SkillLevel DefendingLevel { get; set; }

        public short DefendingLevelDelta { get; set; }

        public SkillLevel ExperienceLevel { get; set; }

        public short ExperienceLevelDelta { get; set; }

        public string FirstName { get; set; }

        public SkillLevel FormLevel { get; set; }

        public short FormLevelDelta { get; set; }

        public string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.NickName)
                     ? $"{this.FirstName} {this.LastName}"
                     : $"{this.FirstName} \"{this.NickName}\" {this.LastName}";
            }
        }

        public short GoalsOnTeam { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public long HattrickId { get; set; }

        public short HealthStatus { get; set; }

        public HonestyLevel HonestyLevel { get; set; }

        public bool IsTransferListed { get; set; }

        public SkillLevel KeeperLevel { get; set; }

        public short KeeperLevelDelta { get; set; }

        public string LastName { get; set; }

        public SkillLevel LeadershipLevel { get; set; }

        public SkillLevel LoyaltyLevel { get; set; }

        public short LoyaltyLevelDelta { get; set; }

        public short MatchesOnTeam { get; set; }

        public string? NickName { get; set; }

        public SkillLevel PassingLevel { get; set; }

        public short PassingLevelDelta { get; set; }

        public SkillLevel PlaymakingLevel { get; set; }

        public short PlaymakingLevelDelta { get; set; }

        public long Salary { get; set; }

        public SkillLevel ScoringLevel { get; set; }

        public short ScoringLevelDelta { get; set; }

        public short SeasonCupGoals { get; set; }

        public short SeasonFriendlyGoals { get; set; }

        public short SeasonLeagueGoals { get; set; }

        public SkillLevel SetPiecesLevel { get; set; }

        public short SetPiecesLevelDelta { get; set; }

        public byte? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public SkillLevel StaminaLevel { get; set; }

        public short StaminaLevelDelta { get; set; }

        public long TotalSkillIndex { get; set; }

        public SkillLevel WingerLevel { get; set; }

        public short WingerLevelDelta { get; set; }
    }
}