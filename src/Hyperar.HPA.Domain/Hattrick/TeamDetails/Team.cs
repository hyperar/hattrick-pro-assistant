namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public string ShortTeamName { get; set; } = string.Empty;

        public bool IsPrimaryClub { get; set; }

        public DateTime FoundedDate { get; set; }

        public Arena Arena { get; set; } = new Arena();

        public League League { get; set; } = new League();

        public Country Country { get; set; } = new Country();

        public Region Region { get; set; } = new Region();

        public Trainer Trainer { get; set; } = new Trainer();

        public string HomePage { get; set; } = string.Empty;

        public string DressUri { get; set; } = string.Empty;

        public string DressAlternateUri { get; set; } = string.Empty;

        public LeagueLevelUnit LeagueLevelUnit { get; set; } = new LeagueLevelUnit();

        public BotStatus BotStatus { get; set; } = new BotStatus();

        public Cup? Cup { get; set; } = new Cup();

        public Rating PowerRating { get; set; } = new Rating();

        public uint? FriendlyTeamId { get; set; }

        public uint? NumberOfVictories { get; set; }

        public uint? NumberOfUndefeated { get; set; }

        public uint? TeamRank { get; set; }

        public Fanclub Fanclub { get; set; } = new Fanclub();

        public string LogoUrl { get; set; } = string.Empty;

        public Guestbook Guestbook { get; set; } = new Guestbook();

        public PressAnnouncement? PressAnnouncement { get; set; }

        public TeamColors? TeamColors { get; set; } = null;

        public uint YouthTeamId { get; set; }

        public string YouthTeamName { get; set; } = string.Empty;

        public uint NumberOfVisits { get; set; }

        public Flags Flags { get; set; } = new Flags();

        public List<Trophy>? TrophyList { get; set; } = null;

        public SupportedTeams SupportedTeams { get; set; } = new SupportedTeams();

        public MySupporters MySupporters { get; set; } = new MySupporters();

        public bool PossibleToChallengeMidweek { get; set; }

        public bool PossibleToChallengeWeekend { get; set; }
    }
}
