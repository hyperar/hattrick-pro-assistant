namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class SupportedTeams
    {
        public uint MaxItems { get; set; }

        public List<SupportedTeam> SupportedTeamList { get; set; } = new List<SupportedTeam>();

        public uint TotalItems { get; set; }
    }
}