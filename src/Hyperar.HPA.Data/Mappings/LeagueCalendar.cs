namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class LeagueCalendar : Entity<Domain.Database.LeagueCalendar>, IEntityMapping<Domain.Database.LeagueCalendar>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Database.LeagueCalendar> builder)
        {
            builder.Property(x => x.NextTrainingUpdate)
                .HasColumnName(Constants.ColumnName.NextTrainingUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.NextEconomyUpdate)
                .HasColumnName(Constants.ColumnName.NextEconomyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.NextCupMatchDate)
                .HasColumnName(Constants.ColumnName.NextCupMatchDate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.NextSeriesMatchDate)
                .HasColumnName(Constants.ColumnName.NextSeriesMatchDate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.FirstWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.FirstWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.SecondWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.SecondWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.ThirdWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.ThirdWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.FourthWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.FourthWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(x => x.FifthWeeklyUpdate)
                .HasColumnName(Constants.ColumnName.FifthWeeklyUpdate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Database.LeagueCalendar> builder)
        {
            builder.HasOne(x => x.League)
                .WithOne(x => x.LeagueCalendar)
                .HasForeignKey<Domain.Database.LeagueCalendar>(x => x.LeagueId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Database.LeagueCalendar> builder)
        {
            builder.ToTable(Constants.TableName.LeagueCalendar);
        }
    }
}
