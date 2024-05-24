namespace Hyperar.HPA.Shared.Models.Hattrick.Avatars
{
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.Players = new List<Player>();
        }

        public List<Player> Players { get; set; }

        public long TeamId { get; set; }
    }
}