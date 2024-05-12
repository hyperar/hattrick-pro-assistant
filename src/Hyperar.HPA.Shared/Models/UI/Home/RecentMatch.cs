namespace Hyperar.HPA.Shared.Models.UI.Home
{
    using Shared.Enums;

    public class RecentMatch
    {
        public RecentMatch()
        {
            this.HomeTeam = new MatchTeam();
            this.AwayTeam = new MatchTeam();
        }

        public MatchTeam AwayTeam { get; set; }

        public byte AwayTeamScore { get; set; }

        public DateTime Date { get; set; }

        public long HattrickId { get; set; }

        public MatchTeam HomeTeam { get; set; }

        public byte HomeTeamScore { get; set; }

        public MatchType Type { get; set; }
    }
}