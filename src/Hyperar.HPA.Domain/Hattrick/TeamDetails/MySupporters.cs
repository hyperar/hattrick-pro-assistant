namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class MySupporters
    {
        public uint TotalItems { get; set; }

        public uint MaxItems { get; set; }

        public List<SupporterTeam> SupporterTeamList { get; set; } = new List<SupporterTeam>();
    }
}
