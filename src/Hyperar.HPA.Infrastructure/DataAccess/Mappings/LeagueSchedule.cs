namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class LeagueSchedule : EntityBase<Domain.LeagueSchedule>, IEntityTypeConfiguration<Domain.LeagueSchedule>, IEntityMapping<Domain.LeagueSchedule>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.LeagueSchedule> builder)
        {
            builder.Property(p => p.NextTrainingUpdate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.NextEconomyUpdate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.NextSeriesMatchDate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.NextCupMatchDate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.FirstDailyUpdate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.SecondDailyUpdate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.ThirdDailyUpdate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.FourthDailyUpdate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.FifthDailyUpdate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.LeagueSchedule> builder)
        {
            builder.HasOne(m => m.League)
                .WithOne(m => m.Schedule)
                .HasConstraintName("FK_LeagueSchedule_League")
                .HasForeignKey<Domain.LeagueSchedule>(m => m.LeagueHattrickId)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.LeagueSchedule> builder)
        {
            builder.ToTable(Constants.TableName.LeagueSchedule);
        }
    }
}