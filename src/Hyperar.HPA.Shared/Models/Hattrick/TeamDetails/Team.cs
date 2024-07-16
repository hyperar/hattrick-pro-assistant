namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public IdName Arena { get; set; } = new IdName();

        public BotStatus BotStatus { get; set; } = new BotStatus();

        public IdName Country { get; set; } = new IdName();

        public Cup? Cup { get; set; }

        public string DressAlternateUri { get; set; } = string.Empty;

        public string DressUri { get; set; } = string.Empty;

        public Fanclub Fanclub { get; set; } = new Fanclub();

        public Flags Flags { get; set; } = new Flags();

        public DateTime FoundedDate { get; set; }

        public long? FriendlyTeamId { get; set; }

        public Guestbook Guestbook { get; set; } = new Guestbook();

        public string HomePage { get; set; } = string.Empty;

        public bool IsPrimaryClub { get; set; }

        public IdName League { get; set; } = new IdName();

        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        public string LogoUrl { get; set; } = string.Empty;

        public MySupporters MySupporters { get; set; } = new MySupporters();

        public int? NumberOfUndefeated { get; set; }

        public int? NumberOfVictories { get; set; }

        public int NumberOfVisits { get; set; }

        public bool PossibleToChallengeMidweek { get; set; }

        public bool PossibleToChallengeWeekend { get; set; }

        public Rating PowerRating { get; set; } = new Rating();

        public PressAnnouncement? PressAnnouncement { get; set; }

        public IdName Region { get; set; } = new IdName();

        public string ShortTeamName { get; set; } = string.Empty;

        public SupportedTeams SupportedTeams { get; set; } = new SupportedTeams();

        public TeamColors? TeamColors { get; set; } = null;

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public int? TeamRank { get; set; }

        public Trainer Trainer { get; set; } = new Trainer();

        public List<Trophy>? TrophyList { get; set; }

        public long YouthTeamId { get; set; }

        public string YouthTeamName { get; set; } = string.Empty;
    }
}