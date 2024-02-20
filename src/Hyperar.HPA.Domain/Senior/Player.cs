namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Common.Enums;
    using Domain.Interfaces;

    public class Player : HattrickEntityBase, IHattrickEntity
    {
        public uint AgeDays { get; set; }

        public uint AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public byte[] AvatarBytes { get; set; } = Array.Empty<byte>();

        public virtual ICollection<PlayerAvatarLayer> AvatarLayers { get; set; } = new HashSet<PlayerAvatarLayer>();

        public BookingStatus BookingStatus { get; set; }

        public uint CareerGoals { get; set; }

        public uint CareerHattricks { get; set; }

        public PlayerCategory Category { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public uint CurrentSeasonCupGoals { get; set; }

        public uint CurrentSeasonFriendlyGoals { get; set; }

        public uint CurrentSeasonLeagueGoals { get; set; }

        public bool EnrolledOnNationalTeam { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public uint GoalsOnTeam { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public int Health { get; set; }

        public HonestyLevel Honesty { get; set; }

        public bool IsCoach { get; set; }

        public bool IsForeign { get; set; }

        public bool IsTransferListed { get; set; }

        public DateTime JoinedTeamOn { get; set; }

        public string LastName { get; set; } = string.Empty;

        public SkillLevel Leadership { get; set; }

        public uint MatchesOnTeam { get; set; }

        public uint NationalTeamCaps { get; set; }

        public string? NickName { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<PlayerSkillSet> PlayerSkillSets { get; set; } = new HashSet<PlayerSkillSet>();

        public uint Salary { get; set; }

        public uint? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public string? Statement { get; set; }

        public virtual Team Team { get; set; } = new Team();

        public uint TotalSkillIndex { get; set; }

        public uint YouthNationalTeamCaps { get; set; }
    }
}