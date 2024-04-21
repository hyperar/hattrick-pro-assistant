namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Manager : HattrickEntityBase<Domain.Manager>, IEntityTypeConfiguration<Domain.Manager>, IEntityMapping<Domain.Manager>
    {
        public override sealed void MapProperties(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.Property(p => p.UserName)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SupporterTier)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.CurrencyName)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(64)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.CurrencyRate)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(10, 5)
                .IsRequired();

            builder.Property(p => p.AvatarBytes)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.VarBinary);
        }

        public override sealed void MapRelationships(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.HasOne(m => m.Country)
                .WithMany(m => m.Managers)
                .HasForeignKey(m => m.CountryHattrickId)
                .HasConstraintName("FK_Manager_Country")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.User)
                .WithOne(m => m.Manager)
                .HasForeignKey<Domain.Manager>(m => m.UserId)
                .HasConstraintName("FK_Manager_User")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override sealed void MapTable(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.ToTable(Constants.TableName.Manager);
        }
    }
}