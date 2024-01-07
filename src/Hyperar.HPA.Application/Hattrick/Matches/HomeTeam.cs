namespace Hyperar.HPA.Application.Hattrick.Matches
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HomeTeam
    {
        public uint HomeTeamId { get; set; }

        public string HomeTeamName { get; set; } = string.Empty;

        public string HomeTeamShortName { get; set; } = string.Empty;
    }
}