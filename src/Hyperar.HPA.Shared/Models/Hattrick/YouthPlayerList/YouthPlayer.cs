namespace Hyperar.HPA.Shared.Models.Hattrick.YouthPlayerList
{
    using System;

    public class YouthPlayer
    {
        public int? Age { get; set; }

        public int? AgeDays { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public int? CanBePromotedIn { get; set; }

        public int? Cards { get; set; }

        public int? CareerGoals { get; set; }

        public int? CareerHattricks { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public int? FriendlyGoals { get; set; }

        public int? InjuryLevel { get; set; }

        public LastMatch? LastMatch { get; set; }

        public string LastName { get; set; } = string.Empty;

        public int? LeagueGoals { get; set; }

        public long? NativeCountryId { get; set; }

        public string? NativeCountryName { get; set; }

        public string? NickName { get; set; }

        public string? OwnerNotes { get; set; }

        public OwningYouthTeam? OwningYouthTeam { get; set; }

        public int? PlayerCategoryId { get; set; }

        public int? PlayerNumber { get; set; }

        public PlayerSkills? PlayerSkills { get; set; }

        public ScoutCall? ScoutCall { get; set; }

        public int? Specialty { get; set; }

        public string? Statement { get; set; }

        public long YouthPlayerId { get; set; }
    }
}