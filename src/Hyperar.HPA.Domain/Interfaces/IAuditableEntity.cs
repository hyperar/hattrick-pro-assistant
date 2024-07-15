namespace Hyperar.HPA.Domain.Interfaces
{
    using System;

    public interface IAuditableEntity
    {
        DateTime CreatedOn { get; set; }

        DateTime? UpdatedOn { get; set; }
    }
}