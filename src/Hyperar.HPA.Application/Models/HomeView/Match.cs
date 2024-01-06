namespace Hyperar.HPA.Application.Models.HomeView
{
    using System;
    using Common.Enums;

    public abstract class Match
    {
        public SeniorTeam AwayTeam { get; set; } = new SeniorTeam();

        public SeniorTeam HomeTeam { get; set; } = new SeniorTeam();

        public MatchType Type { get; set; }
    }
}