namespace Hyperar.HPA.Application.Hattrick.MatchDetails
{
    using Common.Enums;

    public class Arena
    {
        public uint ArenaId { get; set; }

        public string ArenaName { get; set; } = string.Empty;

        public uint? SoldBasic { get; set; }

        public uint? SoldRoof { get; set; }

        public uint? SoldTerraces { get; set; }

        public uint? SoldTotal { get; set; }

        public uint? SoldVip { get; set; }

        public Weather? WeatherId { get; set; }
    }
}