﻿namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    using System;

    public class Player
    {
        public Player()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.PlayerLanguage = string.Empty;
            this.NativeLeagueName = string.Empty;

            this.OwningTeam = new OwningTeam();
            this.MotherClub = new MotherClub();
            this.PlayerSkills = new PlayerSkills();
        }

        public int Age { get; set; }

        public int AgeDays { get; set; }

        public int Aggressiveness { get; set; }

        public int Agreeability { get; set; }

        public DateTime ArrivalDate { get; set; }

        public int Caps { get; set; }

        public int CapsU20 { get; set; }

        public int Cards { get; set; }

        public int CareerGoals { get; set; }

        public int CareerHattricks { get; set; }

        public int CupGoals { get; set; }

        public int Experience { get; set; }

        public string FirstName { get; set; }

        public int FriendliesGoals { get; set; }

        public int GoalsCurrentTeam { get; set; }

        public int Honesty { get; set; }

        public int InjuryLevel { get; set; }

        public bool IsAbroad { get; set; }

        public LastMatch? LastMatch { get; set; }

        public string LastName { get; set; }

        public int Leadership { get; set; }

        public int LeagueGoals { get; set; }

        public int Loyalty { get; set; }

        public int MatchesCurrentTeam { get; set; }

        public MotherClub MotherClub { get; set; }

        public bool MotherClubBonus { get; set; }

        public long NativeCountryId { get; set; }

        public long NativeLeagueId { get; set; }

        public string NativeLeagueName { get; set; }

        public DateTime NextBirthDay { get; set; }

        public string? NickName { get; set; }

        public string? OwnerNotes { get; set; }

        public OwningTeam OwningTeam { get; set; }

        public int PlayerCategoryId { get; set; }

        public int PlayerForm { get; set; }

        public long PlayerId { get; set; }

        public string PlayerLanguage { get; set; }

        public long PlayerLanguageId { get; set; }

        public int? PlayerNumber { get; set; }

        public PlayerSkills PlayerSkills { get; set; }

        public long Salary { get; set; }

        public int Specialty { get; set; }

        public string? Statement { get; set; }

        public TrainerData? TrainerData { get; set; }

        public TransferDetails? TransferDetails { get; set; }

        public bool TransferListed { get; set; }

        public int TSI { get; set; }
    }
}