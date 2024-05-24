namespace Hyperar.HPA.Shared.Models.Hattrick.MatchArchive
{
    public class AwayTeam
    {
        public AwayTeam()
        {
            this.AwayTeamName = string.Empty;
        }

        public long AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }
    }
}