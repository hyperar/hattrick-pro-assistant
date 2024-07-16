namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class League : HattrickEntityBase, IHattrickEntity
    {
        public int ActiveTeams { get; set; }

        public int ActiveUsers { get; set; }

        public string Continent { get; set; } = string.Empty;

        public virtual Country? Country { get; set; }

        public virtual ICollection<LeagueCup> Cups { get; set; } = new HashSet<LeagueCup>();

        public string EnglishName { get; set; } = string.Empty;

        public byte[] FlagBytes { get; set; } = Array.Empty<byte>();

        public long? JuniorNationalTeamHattrickId { get; set; }

        public int LeagueLevels { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual LeagueSchedule? Schedule { get; set; }

        public int Season { get; set; }

        public int SeasonOffset { get; set; }

        public long? SeniorNationalTeamHattrickId { get; set; }

        public virtual ICollection<Senior.Team> SeniorTeams { get; set; } = new HashSet<Senior.Team>();

        public string ShortName { get; set; } = string.Empty;

        public int WaitingUsers { get; set; }

        public int Week { get; set; }

        public string Zone { get; set; } = string.Empty;
    }
}