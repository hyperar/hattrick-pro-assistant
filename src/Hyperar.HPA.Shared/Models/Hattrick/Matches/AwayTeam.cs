namespace Hyperar.HPA.Shared.Models.Hattrick.Matches
{
    public class AwayTeam
    {
        public AwayTeam()
        {
            this.AwayTeamName = string.Empty;
            this.AwayTeamShortName = string.Empty;
        }

        public long AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public string AwayTeamShortName { get; set; }
    }
}