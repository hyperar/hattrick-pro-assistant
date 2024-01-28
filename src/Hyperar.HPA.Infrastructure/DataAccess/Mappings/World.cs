namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class World : EntityBase<Domain.World>, IEntityTypeConfiguration<Domain.World>, IEntityMapping<Domain.World>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.World> builder)
        {
            builder.Property(p => p.Season)
                .HasColumnName(Constants.ColumnName.Season)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Week)
                .HasColumnName(Constants.ColumnName.Week)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.World> builder)
        {
            builder.ToTable(Constants.TableName.World);
        }
    }
}