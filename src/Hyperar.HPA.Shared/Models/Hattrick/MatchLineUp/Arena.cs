namespace Hyperar.HPA.Shared.Models.Hattrick.MatchLineUp
{
    public class Arena
    {
        public Arena()
        {
            this.ArenaName = string.Empty;
        }

        public long ArenaId { get; set; }

        public string ArenaName { get; set; }
    }
}