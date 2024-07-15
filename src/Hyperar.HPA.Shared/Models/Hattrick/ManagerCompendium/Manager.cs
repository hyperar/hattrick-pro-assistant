namespace Hyperar.HPA.Shared.Models.Hattrick.ManagerCompendium
{
    using System.Collections.Generic;

    public class Manager
    {
        public Avatar? Avatar { get; set; }

        public IdName Country { get; set; } = new IdName();

        public Currency Currency { get; set; } = new Currency();

        public IdName Language { get; set; } = new IdName();

        public List<string> LastLogins { get; set; } = new List<string>();

        public string LoginName { get; set; } = string.Empty;

        public List<IdName> NationalTeamAssistant { get; set; } = new List<IdName>();

        public List<IdName> NationalTeamCoach { get; set; } = new List<IdName>();

        public string SupporterTier { get; set; } = string.Empty;

        public List<Team> Teams { get; set; } = new List<Team>();

        public long UserId { get; set; }
    }
}