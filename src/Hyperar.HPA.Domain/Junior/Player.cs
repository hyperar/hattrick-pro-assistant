namespace Hyperar.HPA.Domain.Junior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;

    public class Player : HattrickEntityBase, IHattrickEntity
    {
        public int AgeDays { get; set; }

        public int AgeYears { get; set; }

        public byte[] AvatarBytes { get; set; } = Array.Empty<byte>();

        public BookingStatus BookingStatus { get; set; }

        public int CareerGoals { get; set; }

        public int CareerHattricks { get; set; }

        public PlayerCategory? Category { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public int DaysLeftToPromote { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public int HealthStatus { get; set; }

        public DateTime JoinedOn { get; set; }

        public string LastName { get; set; } = string.Empty;

        public DateTime NextBirthDay { get; set; }

        public string? NickName { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; } = new HashSet<PlayerMatch>();

        public virtual ICollection<PlayerSkillSet> PlayerSkillSets { get; set; } = new HashSet<PlayerSkillSet>();

        public int SeasonFriendlyGoals { get; set; }

        public int SeasonLeagueGoals { get; set; }

        public int? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public string? Statement { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public long TeamHattrickId { get; set; }
    }
}