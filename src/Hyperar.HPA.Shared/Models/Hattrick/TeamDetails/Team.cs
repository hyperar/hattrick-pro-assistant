namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.Arena = new IdName();
            this.BotStatus = new BotStatus();
            this.Country = new IdName();
            this.Fanclub = new Fanclub();
            this.Flags = new Flags();
            this.Guestbook = new Guestbook();
            this.League = new IdName();
            this.LeagueLevelUnit = new LeagueLevelUnit();
            this.MySupporters = new MySupporters();
            this.PowerRating = new Rating();
            this.Region = new IdName();
            this.SupportedTeams = new SupportedTeams();
            this.Trainer = new Trainer();

            this.DressAlternateUri = string.Empty;
            this.DressUri = string.Empty;
            this.HomePage = string.Empty;
            this.LogoUrl = string.Empty;
            this.ShortTeamName = string.Empty;
            this.TeamName = string.Empty;
            this.YouthTeamName = string.Empty;
        }

        public IdName Arena { get; set; }

        public BotStatus BotStatus { get; set; }

        public IdName Country { get; set; }

        public Cup? Cup { get; set; }

        public string DressAlternateUri { get; set; }

        public string DressUri { get; set; }

        public Fanclub Fanclub { get; set; }

        public Flags Flags { get; set; }

        public DateTime FoundedDate { get; set; }

        public long? FriendlyTeamId { get; set; }

        public Guestbook Guestbook { get; set; }

        public string HomePage { get; set; }

        public bool IsPrimaryClub { get; set; }

        public IdName League { get; set; }

        public LeagueLevelUnit LeagueLevelUnit { get; set; }

        public string LogoUrl { get; set; }

        public MySupporters MySupporters { get; set; }

        public int? NumberOfUndefeated { get; set; }

        public int? NumberOfVictories { get; set; }

        public int NumberOfVisits { get; set; }

        public bool PossibleToChallengeMidweek { get; set; }

        public bool PossibleToChallengeWeekend { get; set; }

        public Rating PowerRating { get; set; }

        public PressAnnouncement? PressAnnouncement { get; set; }

        public IdName Region { get; set; }

        public string ShortTeamName { get; set; }

        public SupportedTeams SupportedTeams { get; set; }

        public TeamColors? TeamColors { get; set; } = null;

        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public int? TeamRank { get; set; }

        public Trainer Trainer { get; set; }

        public List<Trophy>? TrophyList { get; set; }

        public long YouthTeamId { get; set; }

        public string YouthTeamName { get; set; }
    }
}