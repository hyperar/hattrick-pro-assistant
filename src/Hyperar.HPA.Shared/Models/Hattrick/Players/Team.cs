namespace Hyperar.HPA.Shared.Models.Hattrick.Players
{
    using System.Collections.Generic;

    public class Team
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public long TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}