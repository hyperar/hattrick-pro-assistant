namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Player : HattrickEntityBase<Domain.Senior.Player>, IEntityTypeConfiguration<Domain.Senior.Player>, IEntityMapping<Domain.Senior.Player>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.Player> builder)
        {
            builder.Property(p => p.FirstName)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.NickName)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(p => p.LastName)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.ShirtNumber)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.IsCoach)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.AgeYears)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AgeDays)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.NextBirthDay)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.JoinedTeamOn)
                .HasColumnOrder(9)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.Notes)
                .HasColumnOrder(10)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(1024)
                .IsUnicode();

            builder.Property(p => p.Statement)
                .HasColumnOrder(11)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(1024)
                .IsUnicode();

            builder.Property(p => p.TotalSkillIndex)
                .HasColumnOrder(12)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.HasMotherClubBonus)
                .HasColumnOrder(13)
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.Salary)
                .HasColumnOrder(14)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.IsForeign)
                .HasColumnOrder(15)
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.Agreeability)
                .HasColumnOrder(16)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Aggressiveness)
                .HasColumnOrder(17)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Honesty)
                .HasColumnOrder(18)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Leadership)
                .HasColumnOrder(19)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Specialty)
                .HasColumnOrder(20)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.IsTransferListed)
                .HasColumnOrder(21)
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.CurrentSeasonLeagueGoals)
                .HasColumnOrder(22)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CurrentSeasonCupGoals)
                .HasColumnOrder(23)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CurrentSeasonFriendlyGoals)
                .HasColumnOrder(24)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CareerGoals)
                .HasColumnOrder(25)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CareerHattricks)
                .HasColumnOrder(26)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.GoalsOnTeam)
                .HasColumnOrder(27)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.MatchesOnTeam)
                .HasColumnOrder(28)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.SeniorNationalTeamCaps)
                .HasColumnOrder(29)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.JuniorNationalTeamCaps)
                .HasColumnOrder(30)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.BookingStatus)
                .HasColumnOrder(31)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Health)
                .HasColumnOrder(32)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Category)
                .HasColumnOrder(33)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AskingPrice)
                .HasColumnOrder(34)
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.BuyingTeamHattrickId)
                .HasColumnOrder(35)
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.BuyingTeamName)
                .HasColumnOrder(36)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256);

            builder.Property(p => p.WinningBid)
                .HasColumnOrder(37)
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.AvatarBytes)
                .HasColumnOrder(38)
                .HasColumnType(Constants.ColumnType.VarBinary);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.Player> builder)
        {
            builder.HasOne(m => m.Country)
                .WithMany(m => m.Players)
                .HasForeignKey(m => m.CountryHattrickId)
                .HasConstraintName("FK_Senior_Player_Country")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Team)
                .WithMany(m => m.Players)
                .HasForeignKey(m => m.TeamHattrickId)
                .HasConstraintName("FK_Senior_Player_Team")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.Player> builder)
        {
            builder.ToTable(Constants.TableName.Player, Constants.Schema.Senior);
        }
    }
}