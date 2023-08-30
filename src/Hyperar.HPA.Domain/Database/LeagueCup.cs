namespace Hyperar.HPA.Domain.Database
{
    using Hyperar.HPA.Domain.Interfaces;

    public class LeagueCup : HattrickEntityBase, IEntity, IHattrickEntity
    {
        public string Name { get; set; } = string.Empty;

        public uint LeagueLevel { get; set; }

        public uint Level { get; set; }

        public uint LevelIndex { get; set; }

        public uint CurrentRound { get; set; }

        public uint RoundsLeft { get; set; }

        public int LeagueId { get; set; }

        public virtual League? League { get; set; }
    }
}
