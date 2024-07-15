namespace Hyperar.HPA.Domain
{
    using System;
    using Domain.Interfaces;

    public abstract class AuditableEntityBase : IAuditableEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}