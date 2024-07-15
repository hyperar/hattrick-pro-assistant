namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;

    internal abstract class AuditableEntityBase<TEntity> : EntityTypeConfigurationBase<TEntity>, IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : Domain.AuditableEntityBase, Domain.Interfaces.IAuditableEntity
    {
    }
}