namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SeniorPlayerSkill : EntityBase<Domain.SeniorPlayerSkill>, IEntityTypeConfiguration<Domain.SeniorPlayerSkill>, IEntityMapping<Domain.SeniorPlayerSkill>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.SeniorPlayerSkill> builder)
        {
            builder.Property(x => x.Season)
                .HasColumnName(Constants.ColumnName.Season)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.Week)
                .HasColumnName(Constants.ColumnName.Week)
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
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.SeniorPlayerSkill> builder)
        {
            builder.HasOne(x => x.SeniorPlayer)
                .WithMany(x => x.SeniorPlayerSkills)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.SeniorPlayerSkill> builder)
        {
            builder.ToTable(Constants.TableName.SeniorPlayerSkill);
        }
    }
}