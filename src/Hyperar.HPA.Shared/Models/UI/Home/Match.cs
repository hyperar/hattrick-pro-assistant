namespace Hyperar.HPA.Shared.Models.UI.Home
{
    using Shared.Enums;

    public class Match
    {
        public Match()
        {
            this.HomeTeam = new MatchTeam();
            this.AwayTeam = new MatchTeam();
        }

        public MatchTeam AwayTeam { get; set; }

        public DateTime Date { get; set; }

        public long HattrickId { get; set; }

        public MatchTeam HomeTeam { get; set; }

        public MatchType Type { get; set; }
    }
}