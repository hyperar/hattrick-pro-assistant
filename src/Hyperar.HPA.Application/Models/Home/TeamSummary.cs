namespace Hyperar.HPA.Application.Models.Home
{
    using System;

    public class TeamOverview
    {
        public Country? Country { get; set; }

        public Manager Manager { get; set; } = new Manager();

        public PlayedMatch[] PlayedMatches { get; set; } = Array.Empty<PlayedMatch>();

        public Region Region { get; set; } = new Region();

        public SeniorPlayer[] SeniorPlayers { get; set; } = Array.Empty<SeniorPlayer>();

        public SeniorSeries SeniorSeries { get; set; } = new SeniorSeries();

        public SeniorTeam SeniorTeam { get; set; } = new SeniorTeam();

        public UpcomingMatch[] UpcomingMatches { get; set; } = Array.Empty<UpcomingMatch>();
    }
}