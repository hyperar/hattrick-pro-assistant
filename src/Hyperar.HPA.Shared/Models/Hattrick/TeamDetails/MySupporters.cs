namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class MySupporters
    {
        public int MaxItems { get; set; }

        public List<SupporterTeam> SupporterTeamList { get; set; } = new List<SupporterTeam>();

        public int TotalItems { get; set; }
    }
}