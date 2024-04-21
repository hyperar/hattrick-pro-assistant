namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class MatchArena : EntityBase, IEntity
    {
        public MatchArena()
        {
            this.Match = new Match();

            this.Name = string.Empty;
        }

        public int? Attendance { get; set; }

        public int? BasicSeatsSold { get; set; }

        public long HattrickId { get; set; }

        public virtual Match Match { get; set; }

        public long MatchHattrickId { get; set; }

        public string Name { get; set; }

        public int? RoofSeatsSold { get; set; }

        public int? TerracesSold { get; set; }

        public int? VipSeatsSold { get; set; }

        public static MatchArena Create(Models.Arena xmlArena, Match match)
        {
            return new MatchArena
            {
                Attendance = xmlArena.SoldTotal,
                BasicSeatsSold = xmlArena.SoldBasic,
                Match = match,
                Name = xmlArena.ArenaName,
                RoofSeatsSold = xmlArena.SoldRoof,
                TerracesSold = xmlArena.SoldTerraces,
                VipSeatsSold = xmlArena.SoldVip
            };
        }

        public bool HasChanged(Models.Arena xmlArena)
        {
            return this.Attendance != xmlArena.SoldTotal
                || this.BasicSeatsSold != xmlArena.SoldBasic
                || this.Name != xmlArena.ArenaName
                || this.RoofSeatsSold != xmlArena.SoldRoof
                || this.TerracesSold != xmlArena.SoldTerraces
                || this.VipSeatsSold != xmlArena.SoldVip;
        }

        public void Update(Models.Arena xmlArena)
        {
            this.Attendance = xmlArena.SoldTotal;
            this.BasicSeatsSold = xmlArena.SoldBasic;
            this.Name = xmlArena.ArenaName;
            this.RoofSeatsSold = xmlArena.SoldRoof;
            this.TerracesSold = xmlArena.SoldTerraces;
            this.VipSeatsSold = xmlArena.SoldVip;
        }
    }
}