namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;

    public class User : EntityBase, IEntity
    {
        public DateTime? LastDownloadDate { get; set; }

        public virtual Manager? Manager { get; set; }

        public long? SelectedTeamHattrickId { get; set; }

        public virtual Token? Token { get; set; }
    }
}