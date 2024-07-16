namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamGoal : AuditableEntityBase<Domain.Junior.MatchTeamGoal>, IEntityTypeConfiguration<Domain.Junior.MatchTeamGoal>, IEntityMapping<Domain.Junior.MatchTeamGoal>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.MatchTeamGoal> builder)
        {
            builder.Property(p => p.Minute)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.PlayerName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.HomeTeamScore)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AwayTeamScore)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.MatchTeamGoal> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithMany(m => m.Goals)
                .HasConstraintName("FK_MatchTeamGoal_MatchTeam")
                .HasForeignKey(m => new { m.TeamHattrickId, m.MatchHattrickId })
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.MatchTeamGoal> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamGoal, Constants.Schema.Junior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Junior.MatchTeamGoal> builder)
        {
            builder.HasKey(p => new { p.TeamHattrickId, p.MatchHattrickId, p.Index });

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

            builder.Property(p => p.Index)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }
    }
}