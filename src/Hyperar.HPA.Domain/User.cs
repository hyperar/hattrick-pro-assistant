namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;

    public class User : EntityBase, IEntity
    {
        public DateTime? LastDownloadDate { get; set; }

        public long? LastSelectedTeamHattrickId { get; set; }

        public virtual Manager? Manager { get; set; }

        public virtual Token? Token { get; set; }
    }
}