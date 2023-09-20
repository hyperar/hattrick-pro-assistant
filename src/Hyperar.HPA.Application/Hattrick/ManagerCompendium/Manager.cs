namespace Hyperar.HPA.Application.Hattrick.ManagerCompendium
{
    using System.Collections.Generic;
    using Hyperar.HPA.Common.Enums;

    public class Manager
    {
        public Avatar? Avatar { get; set; } = null;

        public Country Country { get; set; } = new Country();

        public Currency Currency { get; set; } = new Currency();

        public Language Language { get; set; } = new Language();

        public List<string> LastLogins { get; set; } = new List<string>();

        public string LoginName { get; set; } = string.Empty;

        public List<NationalTeam> NationalTeamAssistant { get; set; } = new List<NationalTeam>();

        public List<NationalTeam> NationalTeamCoach { get; set; } = new List<NationalTeam>();

        public SupporterTier SupporterTier { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();

        public uint UserId { get; set; }
    }
}