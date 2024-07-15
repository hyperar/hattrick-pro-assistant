namespace Hyperar.HPA.Shared.Models.Hattrick.YouthAvatars
{
    using System.Collections.Generic;

    public class YouthTeam
    {
        public List<YouthPlayer> YouthPlayers { get; set; } = new List<YouthPlayer>();

        public long YouthTeamId { get; set; }
    }
}