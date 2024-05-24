namespace Hyperar.HPA.Shared.Models.Hattrick.Players
{
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.PlayerList = new List<Player>();
            this.TeamName = string.Empty;
        }

        public List<Player> PlayerList { get; set; }

        public long TeamId { get; set; }

        public string TeamName { get; set; }
    }
}