namespace Hyperar.HPA.Application.Hattrick.Avatars
{
    using System.Collections.Generic;

    public class Team
    {
        public List<Player> Players { get; set; } = new List<Player>();

        public uint TeamId { get; set; }
    }
}