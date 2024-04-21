namespace Hyperar.HPA.Application.Models.Players
{
    using Shared.Enums;

    public class Player
    {
        public decimal AgeAux
        {
            get
            {
                return decimal.Parse($"{this.AgeYears}{Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator}{this.AgeDays:000}");
            }
        }

        public byte AgeDays { get; set; }

        public byte AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public byte[] Avatar { get; set; } = Array.Empty<byte>();

        public BookingStatus BookingStatus { get; set; }

        public int CareerHattricks { get; set; }

        public int CareerLeagueGoals { get; set; }

        public string CountryName { get; set; } = string.Empty;

        public SkillLevel Defending { get; set; }

        public int? DefendingDelta { get; set; }

        public SkillLevel Experience { get; set; }

        public int? ExperienceDelta { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public SkillLevel Form { get; set; }

        public int? FormDelta { get; set; }

        public string FullName
        {
            get
            {
                return string.IsNullOrEmpty(this.NickName)
                    ? $"{this.FirstName} {this.LastName}"
                    : $"{this.FirstName} \"{this.NickName}\" {this.LastName}";
            }
        }

        public bool HasMotherClubBonus { get; set; }

        public int Health { get; set; }

        public HonestyLevel Honesty { get; set; }

        public long Id { get; set; }

        public SkillLevel Keeper { get; set; }

        public int? KeeperDelta { get; set; }

        public string LastName { get; set; } = string.Empty;

        public SkillLevel Leadership { get; set; }

        public byte[] LeagueFlag { get; set; } = Array.Empty<byte>();

        public SkillLevel Loyalty { get; set; }

        public int? LoyaltyDelta { get; set; }

        public DateTime NextBirthDay
        {
            get
            {
                return DateTime.Today.AddDays(112 - this.AgeDays);
            }
        }

        public string? NickName { get; set; }

        public SkillLevel Passing { get; set; }

        public int? PassingDelta { get; set; }

        public SkillLevel Playmaking { get; set; }

        public int? PlaymakingDelta { get; set; }

        public long Salary { get; set; }

        public SkillLevel Scoring { get; set; }

        public int? ScoringDelta { get; set; }

        public int SeasonCupGoals { get; set; }

        public int SeasonFriendlyGoals { get; set; }

        public int SeasonLeagueGoals { get; set; }

        public SkillLevel SetPieces { get; set; }

        public int? SetPiecesDelta { get; set; }

        public byte? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public SkillLevel Stamina { get; set; }

        public int? StaminaDelta { get; set; }

        public int TeamGoals { get; set; }

        public int TeamMatches { get; set; }

        public string Title
        {
            get
            {
                return this.ShirtNumber.HasValue
                    ? $"{this.ShirtNumber}. {this.FullName} ({this.Id})"
                    : $"{this.FullName} ({this.Id})";
            }
        }

        public int TotalSkillIndex { get; set; }

        public SkillLevel Winger { get; set; }

        public int? WingerDelta { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}