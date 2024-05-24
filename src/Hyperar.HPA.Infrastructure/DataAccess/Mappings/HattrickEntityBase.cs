namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class HattrickEntityBase<TEntity> : EntityTypeConfigurationBase<TEntity>, IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : Domain.HattrickEntityBase, Domain.Interfaces.IHattrickEntity
    {
        protected override sealed void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.HattrickId);

            builder.Property(p => p.HattrickId)
                .HasColumnOrder(0)
                .HasColumnType(Constants.ColumnType.BigInt)
                .ValueGeneratedNever()
                .IsRequired();
        }
    }
}