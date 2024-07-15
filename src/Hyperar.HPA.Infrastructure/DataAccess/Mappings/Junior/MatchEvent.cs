namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchEvent : AuditableEntityBase<Domain.Junior.MatchEvent>, IEntityTypeConfiguration<Domain.Junior.MatchEvent>, IEntityMapping<Domain.Junior.MatchEvent>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.MatchEvent> builder)
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

            builder.Property(p => p.Variation)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.SubjectTeamHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.SubjectPlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.ObjectPlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Text)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NText);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.MatchEvent> builder)
        {
            builder.HasOne(m => m.Match)
                .WithMany(m => m.Events)
                .HasConstraintName("FK_MatchEvent_Match")
                .HasForeignKey(m => m.MatchHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.MatchEvent> builder)
        {
            builder.ToTable(Constants.TableName.MatchEvent, Constants.Schema.Junior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Junior.MatchEvent> builder)
        {
            builder.HasKey(p => new { p.MatchHattrickId, p.Index });

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