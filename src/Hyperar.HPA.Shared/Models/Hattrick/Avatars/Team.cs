namespace Hyperar.HPA.Shared.Models.Hattrick.Avatars
{
    using System.Collections.Generic;

    public class Team
    {
        public List<Player> Players { get; set; } = new List<Player>();

        public long TeamId { get; set; }
    }
}