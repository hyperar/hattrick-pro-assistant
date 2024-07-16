namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUp : AuditableEntityBase<Domain.Senior.MatchTeamLineUp>, IEntityTypeConfiguration<Domain.Senior.MatchTeamLineUp>, IEntityMapping<Domain.Senior.MatchTeamLineUp>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithOne(m => m.LineUp)
                .HasConstraintName("FK_MatchTeamLineUp_MatchTeam")
                .HasForeignKey<Domain.Senior.MatchTeamLineUp>(m => new { m.TeamHattrickId, m.MatchHattrickId })
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUp, Constants.Schema.Senior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
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