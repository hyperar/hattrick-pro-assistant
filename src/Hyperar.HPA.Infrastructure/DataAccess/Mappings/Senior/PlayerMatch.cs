namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PlayerMatch : EntityBase<Domain.Senior.PlayerMatch>, IEntityTypeConfiguration<Domain.Senior.PlayerMatch>, IEntityMapping<Domain.Senior.PlayerMatch>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.PlayerMatch> builder)
        {
            builder.Property(p => p.Date)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.MatchHattrickId)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Role)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AverageRating)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(4, 1)
                .IsRequired();

            builder.Property(p => p.EndOfMatchRating)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(4, 1)
                .IsRequired();

            builder.Property(p => p.CalculatedRating)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(50)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.PlayerMatch> builder)
        {
            builder.HasOne(m => m.Player)
                .WithMany(m => m.Matches)
                .HasForeignKey(m => m.PlayerHattrickId)
                .HasConstraintName("FK_PlayerMatch_Player")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.PlayerMatch> builder)
        {
            builder.ToTable(Constants.TableName.PlayerMatch, Constants.Schema.Senior);
        }
    }
}