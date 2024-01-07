namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class League : HattrickEntityBase<Domain.League>, IEntityTypeConfiguration<Domain.League>, IEntityMapping<Domain.League>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.League> builder)
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

            builder.Property(x => x.EnglishName)
                .HasColumnName(Constants.ColumnName.EnglishName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Continent)
                .HasColumnName(Constants.ColumnName.Continent)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Zone)
                .HasColumnName(Constants.ColumnName.Zone)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Season)
                .HasColumnName(Constants.ColumnName.Season)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.SeasonOffset)
                .HasColumnName(Constants.ColumnName.SeasonOffset)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CurrentRound)
                .HasColumnName(Constants.ColumnName.CurrentRound)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.LanguageId)
                .HasColumnName(Constants.ColumnName.LanguageId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.LanguageName)
                .HasColumnName(Constants.ColumnName.LanguageName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SeniorNationalTeamId)
                .HasColumnName(Constants.ColumnName.SeniorNationalTeamId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.JuniorNationalTeamId)
                .HasColumnName(Constants.ColumnName.JuniorNationalTeamId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.ActiveTeams)
                .HasColumnName(Constants.ColumnName.ActiveTeams)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.ActiveUsers)
                .HasColumnName(Constants.ColumnName.ActiveUsers)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.WaitingUsers)
                .HasColumnName(Constants.ColumnName.WaitingUsers)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.NumberOfLevels)
                .HasColumnName(Constants.ColumnName.NumberOfLevels)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.NextTrainingUpdate)
                .HasColumnName(Constants.ColumnName.NextTrainingUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.NextEconomyUpdate)
                .HasColumnName(Constants.ColumnName.NextEconomyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.NextCupMatchDate)
                .HasColumnName(Constants.ColumnName.NextCupMatchDate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.NextSeriesMatchDate)
                .HasColumnName(Constants.ColumnName.NextSeriesMatchDate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.FirstWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.FirstWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.SecondWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.SecondWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.ThirdWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.ThirdWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.FourthWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.FourthWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.FifthWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.FifthWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.League> builder)
        {
            builder.ToTable(Constants.TableName.League);
        }
    }
}