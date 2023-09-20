namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class MySupporters
    {
        public uint MaxItems { get; set; }

        public List<SupporterTeam> SupporterTeamList { get; set; } = new List<SupporterTeam>();

        public uint TotalItems { get; set; }
    }
}