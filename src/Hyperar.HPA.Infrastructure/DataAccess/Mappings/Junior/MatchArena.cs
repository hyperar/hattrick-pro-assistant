namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchArena : AuditableEntityBase<Domain.Junior.MatchArena>, IEntityTypeConfiguration<Domain.Junior.MatchArena>, IEntityMapping<Domain.Junior.MatchArena>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.MatchArena> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Weather)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AttendanceTerraces)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AttendanceBasic)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AttendanceRoof)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AttendanceVip)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AttendanceTotal)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.MatchArena> builder)
        {
            builder.HasOne(m => m.Match)
                .WithOne(m => m.Arena)
                .HasConstraintName("FK_MatchArena_Match")
                .HasForeignKey<Domain.Junior.MatchArena>(m => m.MatchHattrickId)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.MatchArena> builder)
        {
            builder.ToTable(Constants.TableName.MatchArena, Constants.Schema.Junior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Junior.MatchArena> builder)
        {
            builder.HasKey(p => new { p.ArenaHattrickId, p.MatchHattrickId });

            builder.Property(p => p.ArenaHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.MatchHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }
    }
}