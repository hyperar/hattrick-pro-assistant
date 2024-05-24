namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Language = new IdName();

            this.Icq = string.Empty;
            this.LoginName = string.Empty;
            this.Name = string.Empty;
            this.SupporterTier = string.Empty;
        }

        public DateTime ActivationDate { get; set; }

        public bool HasManagerLicense { get; set; }

        public string Icq { get; set; }

        public IdName Language { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string LoginName { get; set; }

        public string Name { get; set; }

        public List<NationalTeam>? NationalTeams { get; set; }

        public DateTime SignUpDate { get; set; }

        public string SupporterTier { get; set; }

        public long UserId { get; set; }
    }
}