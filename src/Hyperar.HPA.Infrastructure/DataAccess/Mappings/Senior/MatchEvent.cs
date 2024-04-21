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
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.AdditionalPlayerHattrickId)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.TeamHattrickId)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Type)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();

            builder.Property(p => p.Variation)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();

            builder.Property(p => p.Text)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.NText);

            builder.Property(p => p.Minute)
                .HasColumnOrder(8)
               .HasColumnType(Constants.ColumnType.TinyInt)
               .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnOrder(9)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchEvent> builder)
        {
            builder.HasOne(m => m.Match)
                .WithMany(m => m.Events)
                .HasForeignKey(m => m.MatchHattrickId)
                .HasConstraintName("FK_Senior_MatchEvent_Match")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchEvent> builder)
        {
            builder.ToTable(Constants.TableName.MatchEvent, Constants.Schema.Senior);
        }
    }
}