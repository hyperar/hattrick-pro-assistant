namespace Hyperar.HPA.Domain.Hattrick.ManagerCompendium
{
    using System.Collections.Generic;
    using Hyperar.HPA.Common.Enums;

    public class Manager
    {
        public uint UserId { get; set; }

        public string LoginName { get; set; } = string.Empty;

        public SupporterTier SupporterTier { get; set; }

        public List<string> LastLogins { get; set; } = new List<string>();

        public Language Language { get; set; } = new Language();

        public Country Country { get; set; } = new Country();

        public Currency Currency { get; set; } = new Currency();

        public List<Team> Teams { get; set; } = new List<Team>();

        public List<NationalTeam> NationalTeamCoach { get; set; } = new List<NationalTeam>();

        public List<NationalTeam> NationalTeamAssistant { get; set; } = new List<NationalTeam>();

        public Avatar? Avatar { get; set; } = null;
    }
}
