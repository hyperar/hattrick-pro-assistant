namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class League : HattrickEntityBase<Domain.League>, IEntityTypeConfiguration<Domain.League>, IEntityMapping<Domain.League>
    {
        public override sealed void MapProperties(EntityTypeBuilder<Domain.League> builder)
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

            builder.Property(p => p.EnglishName)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Continent)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Zone)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Season)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Week)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.SeasonOffset)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();

            builder.Property(p => p.LanguageId)
                .HasColumnOrder(9)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.LanguageName)
                .HasColumnOrder(10)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SeniorNationalTeamId)
                .HasColumnOrder(11)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.JuniorNationalTeamId)
                .HasColumnOrder(12)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.ActiveTeams)
                .HasColumnOrder(13)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.ActiveUsers)
                .HasColumnOrder(14)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.WaitingUsers)
                .HasColumnOrder(15)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.NumberOfLevels)
                .HasColumnOrder(16)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.NextTrainingUpdate)
                .HasColumnOrder(17)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.NextEconomyUpdate)
                .HasColumnOrder(18)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.NextCupMatchDate)
                .HasColumnOrder(19)
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.NextSeriesMatchDate)
                .HasColumnOrder(20)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.FlagBytes)
                .HasColumnOrder(21)
                .HasColumnType(Constants.ColumnType.VarBinary);
        }

        public override sealed void MapTable(EntityTypeBuilder<Domain.League> builder)
        {
            builder.ToTable(Constants.TableName.League);
        }
    }
}