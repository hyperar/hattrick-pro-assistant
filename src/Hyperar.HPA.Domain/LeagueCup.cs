namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class LeagueCup : HattrickEntityBase, IHattrickEntity
    {
        public int CurrentRound { get; set; }

        public virtual League? League { get; set; }

        public uint LeagueHattrickId { get; set; }

        public uint LeagueLevel { get; set; }

        public uint Level { get; set; }

        public uint LevelIndex { get; set; }

        public string Name { get; set; } = string.Empty;

        public uint RoundsLeft { get; set; }
    }
}