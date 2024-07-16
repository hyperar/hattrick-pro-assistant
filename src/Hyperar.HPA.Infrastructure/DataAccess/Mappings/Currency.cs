namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Currency : EntityBase<Domain.Currency>, IEntityTypeConfiguration<Domain.Currency>, IEntityMapping<Domain.Currency>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Currency> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(p => p.Rate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(10, 5)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Currency> builder)
        {
            builder.ToTable(Constants.TableName.Currency);
        }
    }
}