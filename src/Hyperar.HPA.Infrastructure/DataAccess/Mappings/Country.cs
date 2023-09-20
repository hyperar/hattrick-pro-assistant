namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Country : HattrickEntityBase<Domain.Country>, IEntityTypeConfiguration<Domain.Country>, IEntityMapping<Domain.Country>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

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

            builder.Property(x => x.Code)
                .HasColumnName(Constants.ColumnName.Code)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(4)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.DateFormat)
                .HasColumnName(Constants.ColumnName.DateFormat)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.TimeFormat)
                .HasColumnName(Constants.ColumnName.TimeFormat)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.HasMany(x => x.Regions)
                .WithOne(x => x.Country);

            builder.HasOne(x => x.League)
                .WithOne(x => x.Country)
                .HasForeignKey<Domain.Country>(x => x.LeagueHattrickId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.ToTable(Constants.TableName.Country);
        }
    }
}