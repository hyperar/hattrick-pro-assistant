namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamGoal : EntityBase<Domain.Senior.MatchTeamGoal>, IEntityTypeConfiguration<Domain.Senior.MatchTeamGoal>, IEntityMapping<Domain.Senior.MatchTeamGoal>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamGoal> builder)
        {
            builder.Property(p => p.Index)
                .HasColumnName(Constants.ColumnName.Index)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnName(Constants.ColumnName.PlayerHattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.PlayerName)
                .HasColumnName(Constants.ColumnName.PlayerName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.HomeTeamScore)
                .HasColumnName(Constants.ColumnName.HomeTeamScore)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.AwayTeamScore)
                .HasColumnName(Constants.ColumnName.AwayTeamScore)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Minute)
               .HasColumnName(Constants.ColumnName.Minute)
               .HasColumnOrder(
                   this.GetCurrentColumnOrder())
               .HasColumnType(Constants.ColumnType.BigInt)
               .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnName(Constants.ColumnName.MatchPart)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamGoal> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.Goals);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamGoal> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamGoal, Constants.Schema.Senior);
        }
    }
}