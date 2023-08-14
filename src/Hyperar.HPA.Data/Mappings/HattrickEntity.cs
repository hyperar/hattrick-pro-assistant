namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class HattrickEntity<TEntity> : Entity<TEntity>, IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : Domain.HattrickEntityBase, Domain.Interfaces.IEntity, Domain.Interfaces.IHattrickEntity
    {
        protected override void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            base.MapBaseProperties(builder);

            builder.Property(p => p.HattrickId)
                .HasColumnName(Constants.ColumnName.HattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }
    }
}