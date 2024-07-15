namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamInjury : AuditableEntityBase<Domain.Senior.MatchTeamInjury>, IEntityTypeConfiguration<Domain.Senior.MatchTeamInjury>, IEntityMapping<Domain.Senior.MatchTeamInjury>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamInjury> builder)
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

            builder.Property(p => p.Type)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamInjury> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithMany(m => m.Injuries)
                .HasConstraintName("FK_MatchTeamInjury_MatchTeam")
                .HasForeignKey(m => new { m.TeamHattrickId, m.MatchHattrickId })
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamInjury> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamInjury, Constants.Schema.Senior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Senior.MatchTeamInjury> builder)
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