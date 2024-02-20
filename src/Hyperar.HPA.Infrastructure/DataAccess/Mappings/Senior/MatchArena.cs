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
                .HasColumnName(Constants.ColumnName.HattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Attendance)
                .HasColumnName(Constants.ColumnName.Attendance)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.TerracesSold)
                .HasColumnName(Constants.ColumnName.TerracesSold)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.BasicSeatsSold)
                .HasColumnName(Constants.ColumnName.BasicSeatsSold)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.RoofSeatsSold)
                .HasColumnName(Constants.ColumnName.RoofSeatsSold)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.VipSeatsSold)
                .HasColumnName(Constants.ColumnName.VipSeatsSold)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchArena> builder)
        {
            builder.HasOne(m => m.Match)
                .WithOne(m => m.Arena)
                .HasForeignKey<Domain.Senior.MatchArena>(m => m.MatchHattrickId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchArena> builder)
        {
            builder.ToTable(Constants.TableName.MatchArena, Constants.Schema.Senior);
        }
    }
}