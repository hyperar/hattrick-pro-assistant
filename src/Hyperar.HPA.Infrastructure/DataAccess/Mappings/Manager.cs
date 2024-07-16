namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Manager : HattrickEntityBase<Domain.Manager>, IEntityTypeConfiguration<Domain.Manager>, IEntityMapping<Domain.Manager>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.Property(p => p.UserName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.SupporterTier)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CurrencyName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.CurrencyRate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(5, 1)
                .IsRequired();

            builder.Property(p => p.AvatarBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.HasOne(m => m.Country)
                .WithMany(m => m.Managers)
                .HasConstraintName("FK_Manager_Country")
                .HasForeignKey(m => m.CountryHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.User)
                .WithOne(m => m.Manager)
                .HasConstraintName("FK_Manager_User")
                .HasForeignKey<Domain.Manager>(m => m.UserId)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.ToTable(Constants.TableName.Manager);
        }
    }
}