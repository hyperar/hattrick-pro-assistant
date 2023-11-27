namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Domain.Interfaces;

    public class SeniorPlayer : HattrickEntityBase, IHattrickEntity
    {
        public uint AgeDays { get; set; }

        public uint AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

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

        public string? NickName { get; set; }

        public string? Notes { get; set; }

        public uint Salary { get; set; }

        public uint SeniorNationalTeamCaps { get; set; }

        public virtual List<SeniorPlayerSkill>? SeniorPlayerSkills { get; set; }

        public virtual SeniorTeam SeniorTeam { get; set; } = new SeniorTeam();

        public uint? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public string? Statement { get; set; }

        public uint TotalSkillIndex { get; set; }

        public uint YouthNationalTeamCaps { get; set; }
    }
}