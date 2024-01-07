namespace Hyperar.HPA.Application.Hattrick.Players
{
    using System;
    using Common.Enums;

    public class Player
    {
        public uint Age { get; set; }

        public uint AgeDays { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public DateTime ArrivalDate { get; set; }

        public uint Caps { get; set; }

        public uint CapsU20 { get; set; }

        public uint Cards { get; set; }

        public int CareerGoals { get; set; }

        public int CareerHattricks { get; set; }

        public uint CountryId { get; set; }

        public int CupGoals { get; set; }

        public SkillLevel DefenderSkill { get; set; }

        public SkillLevel Experience { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public int FriendliesGoals { get; set; }

        public int GoalsCurrentTeam { get; set; }

        public HonestyLevel Honesty { get; set; }

        public int InjuryLevel { get; set; }

        public bool IsAbroad { get; set; }

        public SkillLevel KeeperSkill { get; set; }

        public LastMatch? LastMatch { get; set; }

        public string LastName { get; set; } = string.Empty;

        public SkillLevel Leadership { get; set; }

        public int LeagueGoals { get; set; }

        public SkillLevel Loyalty { get; set; }

        public uint MatchesCurrentTeam { get; set; }

        public bool MotherClubBonus { get; set; }

        public uint? NationalTeamId { get; set; }

        public string? NickName { get; set; } = string.Empty;

        public string? OwnerNotes { get; set; }

        public SkillLevel PassingSkill { get; set; }

        public PlayerCategory PlayerCategoryId { get; set; }

        public SkillLevel PlayerForm { get; set; }

        public uint PlayerId { get; set; }

        public uint? PlayerNumber { get; set; }

        public SkillLevel PlaymakerSkill { get; set; }

        public uint Salary { get; set; }

        public SkillLevel ScorerSkill { get; set; }

        public SkillLevel SetPiecesSkill { get; set; }

        public Specialty Specialty { get; set; }

        public SkillLevel StaminaSkill { get; set; }

        public string? Statement { get; set; } = string.Empty;

        public TrainerData? TrainerData { get; set; }

        public bool TransferListed { get; set; }

        public uint Tsi { get; set; }

        public SkillLevel WingerSkill { get; set; }
    }
}