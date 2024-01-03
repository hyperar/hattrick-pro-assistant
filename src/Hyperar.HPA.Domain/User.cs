namespace Hyperar.HPA.Domain
{
    using System;
    using Hyperar.HPA.Domain.Interfaces;

    public class User : EntityBase, IEntity
    {
        public uint? DefaultTeamId { get; set; }

        public DateTime? LastDownloadDate { get; set; }

        public virtual Manager? Manager { get; set; }

        public virtual Token? Token { get; set; }
    }
}