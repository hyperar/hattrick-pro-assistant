namespace Hyperar.HPA.Application.Models.Players
{
    using Common.Enums;

    public class SeniorPlayer
    {
        public uint AgeDays { get; set; }

        public uint AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public byte[] Avatar { get; set; } = Array.Empty<byte>();

        public BookingStatus BookingStatus { get; set; }

        public uint CareerHattricks { get; set; }

        public uint CareerLeagueGoals { get; set; }

        public SkillLevel Defending { get; set; }

        public SkillLevel Experience { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public SkillLevel Form { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public int Health { get; set; }

        public HonestyLevel Honesty { get; set; }

        public uint Id { get; set; }

        public SkillLevel Keeper { get; set; }

        public string LastName { get; set; } = string.Empty;

        public SkillLevel Leadership { get; set; }

        public SkillLevel Loyalty { get; set; }

        public string? NickName { get; set; }

        public SkillLevel Passing { get; set; }

        public SkillLevel Playmaking { get; set; }

        public uint Salary { get; set; }

        public SkillLevel Scoring { get; set; }

        public uint SeasonCupGoals { get; set; }

        public uint SeasonFriendlyGoals { get; set; }

        public uint SeasonLeagueGoals { get; set; }

        public SkillLevel SetPieces { get; set; }

        public uint? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public SkillLevel Stamina { get; set; }

        public uint TeamGoals { get; set; }

        public uint TeamMatches { get; set; }

        public uint TotalSkillIndex { get; set; }

        public SkillLevel Winger { get; set; }
    }
}