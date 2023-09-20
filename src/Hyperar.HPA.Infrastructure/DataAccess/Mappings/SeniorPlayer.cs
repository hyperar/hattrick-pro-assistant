namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SeniorPlayer : HattrickEntityBase<Domain.SeniorPlayer>, IEntityTypeConfiguration<Domain.SeniorPlayer>, IEntityMapping<Domain.SeniorPlayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.SeniorPlayer> builder)
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
                .HasColumnType(Constants.ColumnType.Date)
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

            builder.Property(p => p.SeniorNationalTeamCaps)
                .HasColumnName(Constants.ColumnName.SeniorNationalTeamCaps)
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

            builder.Property(p => p.Loyalty)
                .HasColumnName(Constants.ColumnName.Loyalty)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Form)
                .HasColumnName(Constants.ColumnName.Form)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Stamina)
                .HasColumnName(Constants.ColumnName.Stamina)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Keeper)
                .HasColumnName(Constants.ColumnName.Keeper)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Defending)
                .HasColumnName(Constants.ColumnName.Defending)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Playmaking)
                .HasColumnName(Constants.ColumnName.Playmaking)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Winger)
                .HasColumnName(Constants.ColumnName.Winger)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Passing)
                .HasColumnName(Constants.ColumnName.Passing)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Scoring)
                .HasColumnName(Constants.ColumnName.Scoring)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.SetPieces)
                .HasColumnName(Constants.ColumnName.SetPieces)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Experience)
                .HasColumnName(Constants.ColumnName.Experience)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Category)
                .HasColumnName(Constants.ColumnName.Category)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.SeniorPlayer> builder)
        {
            builder.HasOne(x => x.Country)
                .WithMany(x => x.SeniorPlayers);

            builder.HasOne(x => x.SeniorTeam)
                .WithMany(x => x.SeniorPlayers)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.SeniorPlayer> builder)
        {
            builder.ToTable(Constants.TableName.SeniorPlayer);
        }
    }
}