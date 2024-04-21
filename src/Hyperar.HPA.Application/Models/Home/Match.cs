namespace Hyperar.HPA.Application.Models.Home
{
    using Shared.Enums;

    public abstract class Match
    {
        public Team AwayTeam { get; set; } = new Team();

        public DateTime Date { get; set; }

        public Team HomeTeam { get; set; } = new Team();

        public MatchType Type { get; set; }
    }
}