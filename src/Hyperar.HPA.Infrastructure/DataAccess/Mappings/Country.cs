namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Country : HattrickEntityBase<Domain.Country>, IEntityTypeConfiguration<Domain.Country>, IEntityMapping<Domain.Country>
    {
        public override sealed void MapProperties(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.CurrencyName)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(64)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.CurrencyRate)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(10, 5)
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(4)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.DateFormat)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.TimeFormat)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode();
        }

        public override sealed void MapRelationships(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.HasOne(m => m.League)
                .WithOne(m => m.Country)
                .HasForeignKey<Domain.Country>(x => x.LeagueHattrickId)
                .HasConstraintName("FK_Country_League")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override sealed void MapTable(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.ToTable(Constants.TableName.Country);
        }
    }
}