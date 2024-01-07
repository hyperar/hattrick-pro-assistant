namespace Hyperar.HPA.Application.Models.Home
{
    using Common.Enums;

    public abstract class Match
    {
        public DateTime Date { get; set; }

        public SeniorTeam AwayTeam { get; set; } = new SeniorTeam();

        public SeniorTeam HomeTeam { get; set; } = new SeniorTeam();

        public MatchType Type { get; set; }
    }
}