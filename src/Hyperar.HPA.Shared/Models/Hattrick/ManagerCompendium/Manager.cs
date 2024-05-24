namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    using System.Collections.Generic;

    public class Manager
    {
        public Manager()
        {
            this.Country = new IdName();
            this.Currency = new Currency();
            this.Language = new IdName();
            this.LastLogins = new List<string>();
            this.LoginName = string.Empty;
            this.NationalTeamAssistant = new List<IdName>();
            this.NationalTeamCoach = new List<IdName>();
            this.Teams = new List<Team>();

            this.SupporterTier = string.Empty;
        }

        public Avatar? Avatar { get; set; }

        public IdName Country { get; set; }

        public Currency Currency { get; set; }

        public IdName Language { get; set; }

        public List<string> LastLogins { get; set; }

        public string LoginName { get; set; }

        public List<IdName> NationalTeamAssistant { get; set; }

        public List<IdName> NationalTeamCoach { get; set; }

        public string SupporterTier { get; set; }

        public List<Team> Teams { get; set; }

        public long UserId { get; set; }
    }
}