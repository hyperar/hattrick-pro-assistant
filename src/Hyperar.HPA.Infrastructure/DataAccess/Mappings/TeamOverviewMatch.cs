namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TeamOverviewMatch : HattrickEntityBase<Domain.TeamOverviewMatch>, IEntityTypeConfiguration<Domain.TeamOverviewMatch>, IEntityMapping<Domain.TeamOverviewMatch>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.TeamOverviewMatch> builder)
        {
            builder.Property(p => p.HomeTeamHattrickId)
                .HasColumnName(Constants.ColumnName.HomeTeamHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.HomeTeamName)
                .HasColumnName(Constants.ColumnName.HomeTeamName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.HomeTeamShortName)
                .HasColumnName(Constants.ColumnName.HomeTeamShortName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.HomeGoals)
                .HasColumnName(Constants.ColumnName.HomeGoals)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.AwayTeamHattrickId)
                .HasColumnName(Constants.ColumnName.AwayTeamHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.AwayTeamName)
                .HasColumnName(Constants.ColumnName.AwayTeamName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.AwayTeamShortName)
                .HasColumnName(Constants.ColumnName.AwayTeamShortName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.AwayGoals)
                .HasColumnName(Constants.ColumnName.AwayGoals)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(x => x.StartsOn)
                .HasColumnName(Constants.ColumnName.StartsOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnName(Constants.ColumnName.Type)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.CompetitionId)
                .HasColumnName(Constants.ColumnName.CompetitionId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Status)
                .HasColumnName(Constants.ColumnName.Status)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.TeamOverviewMatch> builder)
        {
            builder.HasOne(x => x.Team)
                .WithMany(x => x.OverviewMatches)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.TeamOverviewMatch> builder)
        {
            builder.ToTable(Constants.TableName.TeamOverviewMatch);
        }
    }
}