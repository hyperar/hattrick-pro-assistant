namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SeniorTeam : HattrickEntityBase<Domain.SeniorTeam>, IEntityTypeConfiguration<Domain.SeniorTeam>, IEntityMapping<Domain.SeniorTeam>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.SeniorTeam> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.ShortName)
                .HasColumnName(Constants.ColumnName.ShortName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.IsPrimary)
                .HasColumnName(Constants.ColumnName.IsPrimary)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(x => x.FoundedOn)
                .HasColumnName(Constants.ColumnName.FoundedOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(p => p.CoachPlayerId)
                .HasColumnName(Constants.ColumnName.CoachPlayerId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.IsPlayingCup)
                .HasColumnName(Constants.ColumnName.IsPlayingCup)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.GlobalRanking)
                .HasColumnName(Constants.ColumnName.GlobalRanking)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.LeagueRanking)
                .HasColumnName(Constants.ColumnName.LeagueRanking)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.RegionRanking)
                .HasColumnName(Constants.ColumnName.RegionRanking)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.PowerRanking)
                .HasColumnName(Constants.ColumnName.PowerRanking)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.TeamRank)
                .HasColumnName(Constants.ColumnName.TeamRank)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.UndefeatedStreak)
                .HasColumnName(Constants.ColumnName.UndefeatedStreak)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.WinStreak)
                .HasColumnName(Constants.ColumnName.WinStreak)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.SeniorTeam> builder)
        {
            builder.HasOne(x => x.Manager)
                .WithMany(x => x.SeniorTeams);

            builder.HasOne(x => x.League)
                .WithMany(x => x.SeniorTeams);

            builder.HasOne(x => x.SeniorTeamArena)
                .WithOne(x => x.SeniorTeam)
                .HasForeignKey<Domain.SeniorTeamArena>(x => x.SeniorTeamHattrickId);

            builder.HasOne(x => x.Region)
                .WithMany(x => x.SeniorTeams)
                .HasForeignKey(x => x.RegionHattrickId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.SeniorTeam> builder)
        {
            builder.ToTable(Constants.TableName.SeniorTeam);
        }
    }
}