namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.Common.Enums;

    public class User
    {
        public DateTime ActivationDate { get; set; }

        public bool HasManagerLicense { get; set; }

        public string Icq { get; set; } = string.Empty;

        public Language Language { get; set; } = new Language();

        public DateTime LastLoginDate { get; set; }

        public string LoginName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<NationalTeam>? NationalTeams { get; set; }

        public DateTime SignUpDate { get; set; }

        public SupporterTier SupporterTier { get; set; }

        public string SupporterTierString { get; set; } = string.Empty;

        public uint UserId { get; set; }
    }
}