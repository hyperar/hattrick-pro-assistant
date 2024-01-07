namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : class
    {
        private int columnOrder;

        public EntityTypeConfigurationBase()
        {
            this.columnOrder = 0;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.MapTable(builder);
            this.MapBaseProperties(builder);
            this.MapProperties(builder);
            this.MapRelationships(builder);
        }

        public abstract void MapProperties(EntityTypeBuilder<TEntity> builder);

        public virtual void MapRelationships(EntityTypeBuilder<TEntity> builder)
        { }

        public abstract void MapTable(EntityTypeBuilder<TEntity> builder);

        protected int GetCurrentColumnOrder()
        {
            return this.columnOrder++;
        }

        protected abstract void MapBaseProperties(EntityTypeBuilder<TEntity> builder);
    }
}