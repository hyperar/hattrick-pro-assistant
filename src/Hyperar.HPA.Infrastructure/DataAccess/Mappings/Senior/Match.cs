namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Match : HattrickEntityBase<Domain.Senior.Match>, IEntityTypeConfiguration<Domain.Senior.Match>, IEntityMapping<Domain.Senior.Match>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.Property(p => p.Date)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.System)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.ContextId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Rules)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AddedMinutes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.Matches)
                .HasConstraintName("FK_Match_Team")
                .HasForeignKey(m => m.TeamHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.ToTable(Constants.TableName.Match, Constants.Schema.Senior);
        }
    }
}