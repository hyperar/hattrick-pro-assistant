namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class LeagueCup : HattrickEntity<Domain.Database.LeagueCup>, IEntityMapping<Domain.Database.LeagueCup>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Database.LeagueCup> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.LeagueLevel)
                .HasColumnName(Constants.ColumnName.LeagueLevel)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Level)
                .HasColumnName(Constants.ColumnName.Level)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.LevelIndex)
                .HasColumnName(Constants.ColumnName.LevelIndex)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.CurrentRound)
                .HasColumnName(Constants.ColumnName.CurrentRound)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.RoundsLeft)
                .HasColumnName(Constants.ColumnName.RoundsLeft)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Database.LeagueCup> builder)
        {
            builder.HasOne(x => x.League)
                .WithMany(x => x.Cups)
                .HasForeignKey(x => x.LeagueId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Database.LeagueCup> builder)
        {
            builder.ToTable(Constants.TableName.LeagueCup);
        }
    }
}
