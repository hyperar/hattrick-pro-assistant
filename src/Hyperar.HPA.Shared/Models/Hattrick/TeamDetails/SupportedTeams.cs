namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class SupportedTeams
    {
        public int MaxItems { get; set; }

        public List<SupportedTeam> SupportedTeamList { get; set; } = new List<SupportedTeam>();

        public int TotalItems { get; set; }
    }
}