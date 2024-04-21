namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System.Collections.Generic;

    public class MySupporters
    {
        public MySupporters()
        {
            this.SupporterTeamList = new List<SupporterTeam>();
        }

        public int MaxItems { get; set; }

        public List<SupporterTeam> SupporterTeamList { get; set; }

        public int TotalItems { get; set; }
    }
}