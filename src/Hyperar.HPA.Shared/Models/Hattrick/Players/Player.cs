namespace Hyperar.HPA.Shared.Models.Hattrick.Players
{
    using System;

    public class Player
    {
        public Player()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public byte Age { get; set; }

        public byte AgeDays { get; set; }

        public byte Aggressiveness { get; set; }

        public byte Agreeability { get; set; }

        public DateTime ArrivalDate { get; set; }

        public short Caps { get; set; }

        public short CapsU20 { get; set; }

        public short Cards { get; set; }

        public short CareerGoals { get; set; }

        public short CareerHattricks { get; set; }

        public long CountryId { get; set; }

        public short CupGoals { get; set; }

        public byte DefenderSkill { get; set; }

        public byte Experience { get; set; }

        public string FirstName { get; set; }

        public short FriendliesGoals { get; set; }

        public short GoalsCurrentTeam { get; set; }

        public byte Honesty { get; set; }

        public short InjuryLevel { get; set; }

        public bool IsAbroad { get; set; }

        public byte KeeperSkill { get; set; }

        public LastMatch? LastMatch { get; set; }

        public string LastName { get; set; }

        public byte Leadership { get; set; }

        public short LeagueGoals { get; set; }

        public byte Loyalty { get; set; }

        public short MatchesCurrentTeam { get; set; }

        public bool MotherClubBonus { get; set; }

        public long? NationalTeamId { get; set; }

        public string? NickName { get; set; }

        public string? OwnerNotes { get; set; }

        public byte PassingSkill { get; set; }

        public byte PlayerCategoryId { get; set; }

        public byte PlayerForm { get; set; }

        public long PlayerId { get; set; }

        public byte? PlayerNumber { get; set; }

        public byte PlaymakerSkill { get; set; }

        public long Salary { get; set; }

        public byte ScorerSkill { get; set; }

        public byte SetPiecesSkill { get; set; }

        public byte Specialty { get; set; }

        public byte StaminaSkill { get; set; }

        public string? Statement { get; set; }

        public TrainerData? TrainerData { get; set; }

        public bool TransferListed { get; set; }

        public int Tsi { get; set; }

        public byte WingerSkill { get; set; }
    }
}