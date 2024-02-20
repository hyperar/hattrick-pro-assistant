namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchEvent : EntityBase<Domain.Senior.MatchEvent>, IEntityTypeConfiguration<Domain.Senior.MatchEvent>, IEntityMapping<Domain.Senior.MatchEvent>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchEvent> builder)
        {
            builder.Property(p => p.Index)
                .HasColumnName(Constants.ColumnName.Index)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnName(Constants.ColumnName.PlayerHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.AdditionalPlayerHattrickId)
                .HasColumnName(Constants.ColumnName.AdditionalPlayerHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.TeamHattrickId)
                .HasColumnName(Constants.ColumnName.TeamHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Type)
                .HasColumnName(Constants.ColumnName.Type)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Variation)
                .HasColumnName(Constants.ColumnName.Variation)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Text)
                .HasColumnName(Constants.ColumnName.Text)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NText)
                .HasMaxLength(8000);

            builder.Property(p => p.Minute)
               .HasColumnName(Constants.ColumnName.Minute)
               .HasColumnOrder(
                   this.GetCurrentColumnOrder())
               .HasColumnType(Constants.ColumnType.BigInt)
               .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnName(Constants.ColumnName.MatchPart)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchEvent> builder)
        {
            builder.HasOne(m => m.Match)
                .WithMany(m => m.Events);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchEvent> builder)
        {
            builder.ToTable(Constants.TableName.MatchEvent, Constants.Schema.Senior);
        }
    }
}