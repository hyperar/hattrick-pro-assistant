namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class SupportedTeams
    {
        public SupportedTeams()
        {
            this.SupportedTeamList = new List<SupportedTeam>();
        }

        public int MaxItems { get; set; }

        public List<SupportedTeam> SupportedTeamList { get; set; }

        public int TotalItems { get; set; }
    }
}