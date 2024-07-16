namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UpcomingMatch : HattrickEntityBase<Domain.Junior.UpcomingMatch>, IEntityTypeConfiguration<Domain.Junior.UpcomingMatch>, IEntityMapping<Domain.Junior.UpcomingMatch>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.UpcomingMatch> builder)
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

            builder.Property(p => p.HomeTeamHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.HomeTeamName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.AwayTeamHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.AwayTeamName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.UpcomingMatch> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.UpcomingMatches)
                .HasConstraintName("FK_UpcomingMatch_Team")
                .HasForeignKey(m => m.TeamHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.UpcomingMatch> builder)
        {
            builder.ToTable(Constants.TableName.UpcomingMatch, Constants.Schema.Junior);
        }
    }
}