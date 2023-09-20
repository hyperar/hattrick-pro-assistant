namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public Arena Arena { get; set; } = new Arena();

        public BotStatus BotStatus { get; set; } = new BotStatus();

        public Country Country { get; set; } = new Country();

        public Cup? Cup { get; set; } = new Cup();

        public string DressAlternateUri { get; set; } = string.Empty;

        public string DressUri { get; set; } = string.Empty;

        public Fanclub Fanclub { get; set; } = new Fanclub();

        public Flags Flags { get; set; } = new Flags();

        public DateTime FoundedDate { get; set; }

        public uint? FriendlyTeamId { get; set; }

        public Guestbook Guestbook { get; set; } = new Guestbook();

        public string HomePage { get; set; } = string.Empty;

        public bool IsPrimaryClub { get; set; }

        public League League { get; set; } = new League();

        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        public string LogoUrl { get; set; } = string.Empty;

        public MySupporters MySupporters { get; set; } = new MySupporters();

        public uint? NumberOfUndefeated { get; set; }

        public uint? NumberOfVictories { get; set; }

        public uint NumberOfVisits { get; set; }

        public bool PossibleToChallengeMidweek { get; set; }

        public bool PossibleToChallengeWeekend { get; set; }

        public Rating PowerRating { get; set; } = new Rating();

        public PressAnnouncement? PressAnnouncement { get; set; }

        public Region Region { get; set; } = new Region();

        public string ShortTeamName { get; set; } = string.Empty;

        public SupportedTeams SupportedTeams { get; set; } = new SupportedTeams();

        public TeamColors? TeamColors { get; set; } = null;

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public uint? TeamRank { get; set; }

        public Trainer Trainer { get; set; } = new Trainer();

        public List<Trophy>? TrophyList { get; set; } = null;

        public uint YouthTeamId { get; set; }

        public string YouthTeamName { get; set; } = string.Empty;
    }
}