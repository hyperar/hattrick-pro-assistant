namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class Entity<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : Domain.EntityBase, Domain.Interfaces.IEntity
    {
        private int columnOrder;

        public Entity()
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

        public abstract void MapRelationships(EntityTypeBuilder<TEntity> builder);

        public abstract void MapTable(EntityTypeBuilder<TEntity> builder);

        protected int GetCurrentColumnOrder()
        {
            return this.columnOrder++;
        }

        protected virtual void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.Id)
                .HasColumnName(Constants.ColumnName.Id)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}