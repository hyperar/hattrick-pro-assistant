namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Team : HattrickEntityBase<Domain.Senior.Team>, IEntityTypeConfiguration<Domain.Senior.Team>, IEntityMapping<Domain.Senior.Team>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.Team> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.ShortName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.FoundedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.IsPrimary)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.IsPlayingCup)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.GlobalRanking)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.LeagueRanking)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.RegionRanking)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.TeamRanking)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.PowerRating)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.UndefeatedStreak)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.WinningStreak)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CanBookMidWeekFriendly)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.CanBookWeekEndFriendly)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.LogoBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();

            builder.Property(p => p.HomeMatchKitBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();

            builder.Property(p => p.AwayMatchKitBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.Team> builder)
        {
            builder.HasOne(m => m.Manager)
                .WithMany(m => m.SeniorTeams)
                .HasConstraintName("FK_Team_Manager")
                .HasForeignKey(m => m.ManagerHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.League)
                .WithMany(m => m.SeniorTeams)
                .HasConstraintName("FK_Team_League")
                .HasForeignKey(m => m.LeagueHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Region)
                .WithMany(m => m.SeniorTeams)
                .HasConstraintName("FK_Team_Region")
                .HasForeignKey(m => m.RegionHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.Team> builder)
        {
            builder.ToTable(Constants.TableName.Team, Constants.Schema.Senior);
        }
    }
}