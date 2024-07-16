namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUp : AuditableEntityBase<Domain.Junior.MatchTeamLineUp>, IEntityTypeConfiguration<Domain.Junior.MatchTeamLineUp>, IEntityMapping<Domain.Junior.MatchTeamLineUp>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.MatchTeamLineUp> builder)
        {
            builder.Property(p => p.Experience)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.PlayStyle)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.MatchTeamLineUp> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithOne(m => m.LineUp)
                .HasConstraintName("FK_MatchTeamLineUp_MatchTeam")
                .HasForeignKey<Domain.Junior.MatchTeamLineUp>(m => new { m.TeamHattrickId, m.MatchHattrickId })
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.MatchTeamLineUp> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUp, Constants.Schema.Junior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Junior.MatchTeamLineUp> builder)
        {
            builder.HasKey(p => new { p.TeamHattrickId, p.MatchHattrickId });

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
        }
    }
}