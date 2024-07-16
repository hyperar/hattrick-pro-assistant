namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : class, Domain.Interfaces.IAuditableEntity
    {
        private int currentColumnOrder;

        protected EntityTypeConfigurationBase()
        {
            this.currentColumnOrder = -1;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.MapTable(builder);
            this.MapBaseProperties(builder);
            this.MapProperties(builder);
            this.MapAuditProperties(builder);
            this.MapRelationships(builder);
        }

        public void MapAuditProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.CreatedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.CreatedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();
        }

        public abstract void MapProperties(EntityTypeBuilder<TEntity> builder);

        public virtual void MapRelationships(EntityTypeBuilder<TEntity> builder)
        { }

        public abstract void MapTable(EntityTypeBuilder<TEntity> builder);

        protected int GetColumnOrder()
        {
            return this.currentColumnOrder++;
        }

        protected abstract void MapBaseProperties(EntityTypeBuilder<TEntity> builder);
    }
}