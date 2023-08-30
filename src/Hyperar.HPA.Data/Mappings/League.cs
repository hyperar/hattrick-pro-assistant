namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class League : HattrickEntity<Domain.Database.League>, IEntityMapping<Domain.Database.League>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Database.League> builder)
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
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Database.League> builder)
        {
        }

        public override void MapTable(EntityTypeBuilder<Domain.Database.League> builder)
        {
            builder.ToTable(Constants.TableName.League);
        }
    }
}
