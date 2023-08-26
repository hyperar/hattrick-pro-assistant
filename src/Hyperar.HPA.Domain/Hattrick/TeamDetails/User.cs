namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HPA.Common.Enums;

    public class User
    {
        public uint UserId { get; set; }

        public Language Language { get; set; } = new Language();

        public string SupporterTierString { get; set; } = string.Empty;

        public SupporterTier SupporterTier { get; set; }

        public string LoginName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Icq { get; set; } = string.Empty;

        public DateTime SignUpDate { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public bool HasManagerLicense { get; set; }

        public List<NationalTeam>? NationalTeams { get; set; }
    }
}
