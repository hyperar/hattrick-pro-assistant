namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUpPlayer : AuditableEntityBase<Domain.Junior.MatchTeamLineUpPlayer>, IEntityTypeConfiguration<Domain.Junior.MatchTeamLineUpPlayer>, IEntityMapping<Domain.Junior.MatchTeamLineUpPlayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpPlayer> builder)
        {
            builder.Property(p => p.FirstName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.NickName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128);

            builder.Property(p => p.LastName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Behavior)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.AverageRating)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(5, 1);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpPlayer> builder)
        {
            builder.HasOne(m => m.MatchTeamLineUp)
                .WithMany(m => m.Players)
                .HasConstraintName("FK_MatchTeamLineUpPlayer_MatchTeamLineUp")
                .HasForeignKey(m => new { m.TeamHattrickId, m.MatchHattrickId })
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpPlayer> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpPlayer, Constants.Schema.Junior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpPlayer> builder)
        {
            builder.HasKey(p => new { p.PlayerHattrickId, p.TeamHattrickId, p.MatchHattrickId, p.Role });

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.TeamHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.MatchHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Role)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }
    }
}