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
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.ShortName)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.IsPrimary)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.FoundedOn)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.CoachPlayerId)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.IsPlayingCup)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.HasPromotedJuniorPlayer)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.GlobalRanking)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.LeagueRanking)
                .HasColumnOrder(9)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.RegionRanking)
                .HasColumnOrder(10)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.PowerRanking)
                .HasColumnOrder(11)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.TeamRank)
                .HasColumnOrder(12)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.UndefeatedStreak)
                .HasColumnOrder(13)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.WinStreak)
                .HasColumnOrder(14)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.SeriesHattrickId)
                .HasColumnOrder(15)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.SeriesName)
                .HasColumnOrder(16)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SeriesDivision)
                .HasColumnOrder(17)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.LogoBytes)
                .HasColumnOrder(18)
                .HasColumnType(Constants.ColumnType.VarBinary);

            builder.Property(p => p.HomeMatchKitBytes)
                .HasColumnOrder(19)
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();

            builder.Property(p => p.AwayMatchKitBytes)
                .HasColumnOrder(20)
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.Team> builder)
        {
            builder.HasOne(m => m.League)
                .WithMany(m => m.Teams)
                .HasForeignKey(m => m.LeagueHattrickId)
                .HasConstraintName("FK_Senior_Team_League")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Manager)
                .WithMany(m => m.Teams)
                .HasForeignKey(m => m.ManagerHattrickId)
                .HasConstraintName("FK_Senior_Team_Manager")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Region)
                .WithMany(m => m.Teams)
                .HasForeignKey(m => m.RegionHattrickId)
                .HasConstraintName("FK_Senior_Team_Region")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.Team> builder)
        {
            builder.ToTable(Constants.TableName.Team, Constants.Schema.Senior);
        }
    }
}