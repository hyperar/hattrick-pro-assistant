namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick.ArenaDetails;

    public class TeamArena : HattrickEntityBase, IHattrickEntity
    {
        public TeamArena()
        {
            this.Team = new Team();

            this.Name = string.Empty;
            this.ImageBytes = Array.Empty<byte>();
        }

        public int BasicSeatCapacity { get; set; }

        public DateTime BuiltOn { get; set; }

        public byte[] ImageBytes { get; set; }

        public string Name { get; set; }

        public int RoofSeatCapacity { get; set; }

        public virtual Team Team { get; set; }

        public long TeamHattrickId { get; set; }

        public int TerracesCapacity { get; set; }

        public int TotalCapacity { get; set; }

        public int VipLoungeCapacity { get; set; }

        public static TeamArena Create(
            Models.Arena xmlArena,
            byte[] imageBytes,
            Team team)
        {
            ArgumentNullException.ThrowIfNull(xmlArena.CurrentCapacity.RebuiltDate, nameof(xmlArena.CurrentCapacity.RebuiltDate));

            return new TeamArena
            {
                BasicSeatCapacity = xmlArena.CurrentCapacity.Basic,
                BuiltOn = xmlArena.CurrentCapacity.RebuiltDate.Value,
                ImageBytes = imageBytes,
                HattrickId = xmlArena.ArenaId,
                Name = xmlArena.ArenaName,
                RoofSeatCapacity = xmlArena.CurrentCapacity.Roof,
                Team = team,
                TeamHattrickId = team.HattrickId,
                TerracesCapacity = xmlArena.CurrentCapacity.Terraces,
                TotalCapacity = xmlArena.CurrentCapacity.Total,
                VipLoungeCapacity = xmlArena.CurrentCapacity.Vip
            };
        }

        public bool HasChanged(Models.Arena xmlArena, byte[] imageBytes)
        {
            ArgumentNullException.ThrowIfNull(xmlArena.CurrentCapacity.RebuiltDate, nameof(xmlArena.CurrentCapacity.RebuiltDate));

            return this.BasicSeatCapacity != xmlArena.CurrentCapacity.Basic
                || this.BuiltOn != xmlArena.CurrentCapacity.RebuiltDate.Value
                || !this.ImageBytes.SequenceEqual(imageBytes)
                || this.Name != xmlArena.ArenaName
                || this.RoofSeatCapacity != xmlArena.CurrentCapacity.Roof
                || this.TerracesCapacity != xmlArena.CurrentCapacity.Terraces
                || this.TotalCapacity != xmlArena.CurrentCapacity.Total
                || this.VipLoungeCapacity != xmlArena.CurrentCapacity.Vip;
        }

        public void Update(Models.Arena xmlArena, byte[] imageBytes)
        {
            ArgumentNullException.ThrowIfNull(xmlArena.CurrentCapacity.RebuiltDate, nameof(xmlArena.CurrentCapacity.RebuiltDate));

            this.BasicSeatCapacity = xmlArena.CurrentCapacity.Basic;
            this.BuiltOn = xmlArena.CurrentCapacity.RebuiltDate.Value;
            this.ImageBytes = imageBytes;
            this.Name = xmlArena.ArenaName;
            this.RoofSeatCapacity = xmlArena.CurrentCapacity.Roof;
            this.TerracesCapacity = xmlArena.CurrentCapacity.Terraces;
            this.TotalCapacity = xmlArena.CurrentCapacity.Total;
            this.VipLoungeCapacity = xmlArena.CurrentCapacity.Vip;
        }
    }
}