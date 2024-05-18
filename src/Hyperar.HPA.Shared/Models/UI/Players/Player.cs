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

            this.MatchesRating = new List<MatchRating>();
        }

        public decimal AgeAux
        {
            get
            {
                return decimal.Parse(
                    $"{this.AgeYears}{Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator}{this.AgeDays:000}");
            }
        }

        public int AgeDays { get; set; }

        public int AgeYears { get; set; }

        public AggressivenessLevel AggressivenessLevel { get; set; }

        public AgreeabilityLevel AgreeabilityLevel { get; set; }

        public long? AskingPrice { get; set; }

        public byte[] AvatarBytes { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public int CareerHattricks { get; set; }

        public int CareerLeagueGoals { get; set; }

        public Country Country { get; set; }

        public SkillLevel DefendingLevel { get; set; }

        public int DefendingLevelDelta { get; set; }

        public SkillLevel ExperienceLevel { get; set; }

        public int ExperienceLevelDelta { get; set; }

        public string FirstName { get; set; }

        public SkillLevel FormLevel { get; set; }

        public int FormLevelDelta { get; set; }

        public string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.NickName)
                     ? $"{this.FirstName} {this.LastName}"
                     : $"{this.FirstName} \"{this.NickName}\" {this.LastName}";
            }
        }

        public int GoalsOnTeam { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public long HattrickId { get; set; }

        public int HealthStatus { get; set; }

        public HonestyLevel HonestyLevel { get; set; }

        public bool IsTransferListed { get; set; }

        public SkillLevel KeeperLevel { get; set; }

        public int KeeperLevelDelta { get; set; }

        public MatchRating? LastMatchRating
        {
            get
            {
                return this.MatchesRating.FirstOrDefault();
            }
        }

        public string LastName { get; set; }

        public SkillLevel LeadershipLevel { get; set; }

        public SkillLevel LoyaltyLevel { get; set; }

        public int LoyaltyLevelDelta { get; set; }

        public int MatchesOnTeam { get; set; }

        public List<MatchRating> MatchesRating { get; set; }

        public DateTime NextBirthDay { get; set; }

        public string? NickName { get; set; }

        public SkillLevel PassingLevel { get; set; }

        public int PassingLevelDelta { get; set; }

        public SkillLevel PlaymakingLevel { get; set; }

        public int PlaymakingLevelDelta { get; set; }

        public long Salary { get; set; }

        public SkillLevel ScoringLevel { get; set; }

        public int ScoringLevelDelta { get; set; }

        public int SeasonCupGoals { get; set; }

        public int SeasonFriendlyGoals { get; set; }

        public int SeasonLeagueGoals { get; set; }

        public SkillLevel SetPiecesLevel { get; set; }

        public int SetPiecesLevelDelta { get; set; }

        public int? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public SkillLevel StaminaLevel { get; set; }

        public int StaminaLevelDelta { get; set; }

        public long TotalSkillIndex { get; set; }

        public SkillLevel WingerLevel { get; set; }

        public int WingerLevelDelta { get; set; }

        public long? WinningBid { get; set; }
    }
}