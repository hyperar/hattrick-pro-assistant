namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public interface IEntityMapping<TEntity> where TEntity : EntityBase, IEntity
    {
        void MapProperties(EntityTypeBuilder<TEntity> builder);

        void MapRelationships(EntityTypeBuilder<TEntity> builder);

        void MapTable(EntityTypeBuilder<TEntity> builder);
    }
}