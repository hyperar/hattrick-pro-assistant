namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Manager : HattrickEntityBase<Domain.Manager>, IEntityTypeConfiguration<Domain.Manager>, IEntityMapping<Domain.Manager>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.Property(p => p.UserName)
                .HasColumnName(Constants.ColumnName.UserName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SupporterTier)
                .HasColumnName(Constants.ColumnName.SupporterTier)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(x => x.CurrencyName)
                .HasColumnName(Constants.ColumnName.CurrencyName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(64)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.CurrencyRate)
                .HasColumnName(Constants.ColumnName.CurrencyRate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(10, 5)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.HasOne(x => x.Country)
                .WithMany(x => x.Managers)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.ToTable(Constants.TableName.Manager);
        }
    }
}