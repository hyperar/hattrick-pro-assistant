namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Country : HattrickEntityBase<Domain.Country>, IEntityTypeConfiguration<Domain.Country>, IEntityMapping<Domain.Country>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.DateFormat)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.TimeFormat)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.HasOne(m => m.Currency)
                .WithMany(m => m.Countries)
                .HasConstraintName("FK_Country_Currency")
                .HasForeignKey(m => m.CurrencyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.League)
                .WithOne(m => m.Country)
                .HasConstraintName("FK_Country_League")
                .HasForeignKey<Domain.Country>(m => m.LeagueHattrickId)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Country> builder)
        {
            builder.ToTable(Constants.TableName.Country);
        }
    }
}