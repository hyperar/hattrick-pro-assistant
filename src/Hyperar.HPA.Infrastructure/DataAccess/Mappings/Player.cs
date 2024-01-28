namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Player : HattrickEntityBase<Domain.Player>, IEntityTypeConfiguration<Domain.Player>, IEntityMapping<Domain.Player>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Player> builder)
        {
            builder.Property(x => x.FirstName)
                .HasColumnName(Constants.ColumnName.FirstName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.NickName)
                .HasColumnName(Constants.ColumnName.NickName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(x => x.LastName)
                .HasColumnName(Constants.ColumnName.LastName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.ShirtNumber)
                .HasColumnName(Constants.ColumnName.ShirtNumber)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(x => x.IsCoach)
                .HasColumnName(Constants.ColumnName.IsCoach)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.AgeYears)
                .HasColumnName(Constants.ColumnName.AgeYears)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.AgeDays)
                .HasColumnName(Constants.ColumnName.AgeDays)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.JoinedTeamOn)
                .HasColumnName(Constants.ColumnName.JoinedTeamOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasColumnName(Constants.ColumnName.Notes)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .IsUnicode();

            builder.Property(x => x.Statement)
                .HasColumnName(Constants.ColumnName.Statement)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .IsUnicode();

            builder.Property(p => p.TotalSkillIndex)
                .HasColumnName(Constants.ColumnName.TotalSkillIndex)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.HasMotherClubBonus)
                .HasColumnName(Constants.ColumnName.HasMotherClubBonus)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.Salary)
                .HasColumnName(Constants.ColumnName.Salary)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.IsForeign)
                .HasColumnName(Constants.ColumnName.IsForeign)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.Agreeability)
                .HasColumnName(Constants.ColumnName.Agreeability)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Aggressiveness)
                .HasColumnName(Constants.ColumnName.Aggressiveness)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Honesty)
                .HasColumnName(Constants.ColumnName.Honesty)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Leadership)
                .HasColumnName(Constants.ColumnName.Leadership)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Specialty)
                .HasColumnName(Constants.ColumnName.Specialty)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.IsTransferListed)
                .HasColumnName(Constants.ColumnName.IsTransferListed)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(x => x.EnrolledOnNationalTeam)
                .HasColumnName(Constants.ColumnName.EnrolledOnNationalTeam)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.CurrentSeasonLeagueGoals)
                .HasColumnName(Constants.ColumnName.CurrentSeasonLeagueGoals)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.CurrentSeasonCupGoals)
                .HasColumnName(Constants.ColumnName.CurrentSeasonCupGoals)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.CurrentSeasonFriendlyGoals)
                .HasColumnName(Constants.ColumnName.CurrentSeasonFriendlyGoals)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.CareerGoals)
                .HasColumnName(Constants.ColumnName.CareerGoals)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.CareerHattricks)
                .HasColumnName(Constants.ColumnName.CareerHattricks)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.GoalsOnTeam)
                .HasColumnName(Constants.ColumnName.GoalsOnTeam)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.MatchesOnTeam)
                .HasColumnName(Constants.ColumnName.MatchesOnTeam)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.NationalTeamCaps)
                .HasColumnName(Constants.ColumnName.NationalTeamCaps)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.YouthNationalTeamCaps)
                .HasColumnName(Constants.ColumnName.YouthNationalTeamCaps)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.BookingStatus)
                .HasColumnName(Constants.ColumnName.BookingStatus)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Health)
                .HasColumnName(Constants.ColumnName.Health)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Category)
                .HasColumnName(Constants.ColumnName.Category)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Avatar)
                .HasColumnName(Constants.ColumnName.Avatar)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Player> builder)
        {
            builder.HasOne(x => x.Country)
                .WithMany(x => x.Players);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Player> builder)
        {
            builder.ToTable(Constants.TableName.Player);
        }
    }
}