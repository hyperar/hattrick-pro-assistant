namespace Hyperar.HPA.Infrastructure.DataAccess.Interfaces
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public interface IEntityMapping<TEntity> where TEntity : class
    {
        void MapAuditProperties(EntityTypeBuilder<TEntity> builder);

        void MapProperties(EntityTypeBuilder<TEntity> builder);

        void MapRelationships(EntityTypeBuilder<TEntity> builder);

        void MapTable(EntityTypeBuilder<TEntity> builder);
    }
}