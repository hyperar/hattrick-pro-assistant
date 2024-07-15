namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class EntityBase<TEntity> : EntityTypeConfigurationBase<TEntity>, IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : Domain.EntityBase, Domain.Interfaces.IEntity, Domain.Interfaces.IAuditableEntity
    {
        protected override sealed void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}