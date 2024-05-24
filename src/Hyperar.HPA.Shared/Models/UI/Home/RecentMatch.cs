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

        public int AwayTeamScore { get; set; }

        public DateTime Date { get; set; }

        public long HattrickId { get; set; }

        public MatchTeam HomeTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public MatchResult Result { get; set; }

        public MatchType Type { get; set; }
    }
}