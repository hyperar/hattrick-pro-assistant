namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public DateTime ActivationDate { get; set; }

        public bool HasManagerLicense { get; set; }

        public string Icq { get; set; } = string.Empty;

        public IdName Language { get; set; } = new IdName();

        public DateTime LastLoginDate { get; set; }

        public string LoginName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<NationalTeam>? NationalTeams { get; set; }

        public DateTime SignUpDate { get; set; }

        public string SupporterTier { get; set; } = string.Empty;

        public long UserId { get; set; }
    }
}