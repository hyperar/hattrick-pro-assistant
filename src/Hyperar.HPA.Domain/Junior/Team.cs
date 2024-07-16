namespace Hyperar.HPA.Domain.Junior
{
    using System;
    using Domain.Interfaces;

    public class Team : HattrickEntityBase, IHattrickEntity
    {
        public DateTime CanBookFriendlyOn { get; set; }

        public DateTime FoundedOn { get; set; }

        public virtual ICollection<Match> Matches { get; set; } = new HashSet<Match>();

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();

        public virtual Senior.Team SeniorTeam { get; set; } = new Senior.Team();

        public long SeniorTeamHattrickId { get; set; }

        public virtual ICollection<Series> Series { get; set; } = new HashSet<Series>();

        public string ShortName { get; set; } = string.Empty;

        public long TrainerPlayerHattrickId { get; set; }

        public virtual ICollection<UpcomingMatch> UpcomingMatches { get; set; } = new HashSet<UpcomingMatch>();
    }
}