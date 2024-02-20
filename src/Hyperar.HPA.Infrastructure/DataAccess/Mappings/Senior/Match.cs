namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Match : HattrickEntityBase<Domain.Senior.Match>, IEntityTypeConfiguration<Domain.Senior.Match>, IEntityMapping<Domain.Senior.Match>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.Property(p => p.SourceSystem)
                   .HasColumnName(Constants.ColumnName.SourceSystem)
                   .HasColumnOrder(
                        this.GetCurrentColumnOrder())
                   .HasColumnType(Constants.ColumnType.NVarChar)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnName(Constants.ColumnName.Type)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.CompetitionId)
                .HasColumnName(Constants.ColumnName.CompetitionId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Rules)
                .HasColumnName(Constants.ColumnName.Rules)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.HomeTeamHattrickId)
                .HasColumnName(Constants.ColumnName.HomeTeamHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.AwayTeamHattrickId)
                .HasColumnName(Constants.ColumnName.AwayTeamHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.StartDate)
                .HasColumnName(Constants.ColumnName.StartDate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.FinishDate)
                .HasColumnName(Constants.ColumnName.FinishDate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.AddedMinutes)
                .HasColumnName(Constants.ColumnName.AddedMinutes)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Weather)
                .HasColumnName(Constants.ColumnName.Weather)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.Matches);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.ToTable(Constants.TableName.Match, Constants.Schema.Senior);
        }
    }
}