namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Country : HattrickEntity<Domain.Database.Country>, IEntityMapping<Domain.Database.Country>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Database.Country> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.Database.Country> builder)
        {
            builder.HasMany(x => x.Regions)
                .WithOne(x => x.Country);

            builder.HasOne(x => x.League)
                .WithOne(x => x.Country)
                .HasForeignKey<Domain.Database.Country>(x => x.LeagueId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Database.Country> builder)
        {
            builder.ToTable(Constants.TableName.Country);
        }
    }
}
