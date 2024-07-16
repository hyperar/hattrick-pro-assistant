namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class Player : HattrickEntityBase, IHattrickEntity
    {
        public int AgeDays { get; set; }

        public int AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public byte[] AvatarBytes { get; set; } = Array.Empty<byte>();

        public BookingStatus BookingStatus { get; set; }

        public int CareerGoals { get; set; }

        public int CareerHattricks { get; set; }

        public PlayerCategory? Category { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public int GoalsOnTeam { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public int HealthStatus { get; set; }

        public HonestyLevel Honesty { get; set; }

        public bool IsForeign { get; set; }

        public bool IsTransferListed { get; set; }

        public DateTime JoinedOn { get; set; }

        public int JuniorNationalTeamCaps { get; set; }

        public string LastName { get; set; } = string.Empty;

        public SkillLevel Leadership { get; set; }

        public int MatchesOnTeam { get; set; }

        public DateTime NextBirthDay { get; set; }

        public string? NickName { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; } = new HashSet<PlayerMatch>();

        public virtual ICollection<PlayerSkillSet> PlayerSkillSets { get; set; } = new HashSet<PlayerSkillSet>();

        public long Salary { get; set; }

        public int SeasonCupGoals { get; set; }

        public int SeasonFriendlyGoals { get; set; }

        public int SeasonLeagueGoals { get; set; }

        public int SeniorNationalTeamCaps { get; set; }

        public int? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public string? Statement { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }

        public int TotalSkillIndex { get; set; }
    }
}