namespace Hyperar.HPA.Application.Hattrick.Avatars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Team
    {
        public List<Player> Players { get; set; } = new List<Player>();

        public uint TeamId { get; set; }
    }
}