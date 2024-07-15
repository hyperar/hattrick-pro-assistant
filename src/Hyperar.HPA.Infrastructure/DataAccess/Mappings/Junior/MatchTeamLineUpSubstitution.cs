namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUpSubstitution : AuditableEntityBase<Domain.Junior.MatchTeamLineUpSubstitution>, IEntityTypeConfiguration<Domain.Junior.MatchTeamLineUpSubstitution>, IEntityMapping<Domain.Junior.MatchTeamLineUpSubstitution>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpSubstitution> builder)
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
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpSubstitution> builder)
        {
            builder.HasOne(m => m.MatchTeamLineUp)
                .WithMany(m => m.Substitutions)
                .HasConstraintName("FK_MatchTeamLineUpSubstitution_MatchTeamLineUp")
                .HasForeignKey(m => new { m.TeamHattrickId, m.MatchHattrickId })
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpSubstitution> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpSubstitution, Constants.Schema.Junior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Junior.MatchTeamLineUpSubstitution> builder)
        {
            builder.HasKey(p => new { p.OutPlayerHattrickId, p.InPlayerHattrickId, p.TeamHattrickId, p.MatchHattrickId, p.NewRole, p.NewBehavior });

            builder.Property(p => p.OutPlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.InPlayerHattrickId)
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

            builder.Property(p => p.NewRole)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.NewBehavior)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }
    }
}