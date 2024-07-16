namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUpSubstitution : AuditableEntityBase<Domain.Senior.MatchTeamLineUpSubstitution>, IEntityTypeConfiguration<Domain.Senior.MatchTeamLineUpSubstitution>, IEntityMapping<Domain.Senior.MatchTeamLineUpSubstitution>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
        {
            builder.HasOne(m => m.MatchTeamLineUp)
                .WithMany(m => m.Substitutions)
                .HasConstraintName("FK_MatchTeamLineUpSubstitution_MatchTeamLineUp")
                .HasForeignKey(m => new { m.TeamHattrickId, m.MatchHattrickId })
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpSubstitution, Constants.Schema.Senior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
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