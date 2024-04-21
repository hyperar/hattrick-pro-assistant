namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchArena : EntityBase<Domain.Senior.MatchArena>, IEntityTypeConfiguration<Domain.Senior.MatchArena>, IEntityMapping<Domain.Senior.MatchArena>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchArena> builder)
        {
            builder.Property(p => p.HattrickId)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Attendance)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.TerracesSold)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.BasicSeatsSold)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.RoofSeatsSold)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.VipSeatsSold)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.Int);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchArena> builder)
        {
            builder.HasOne(m => m.Match)
                .WithOne(m => m.Arena)
                .HasForeignKey<Domain.Senior.MatchArena>(m => m.MatchHattrickId)
                .HasConstraintName("FK_Senior_MatchArena_Match")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchArena> builder)
        {
            builder.ToTable(Constants.TableName.MatchArena, Constants.Schema.Senior);
        }
    }
}