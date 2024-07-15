namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PlayerSkillSet : AuditableEntityBase<Domain.Senior.PlayerSkillSet>, IEntityTypeConfiguration<Domain.Senior.PlayerSkillSet>, IEntityMapping<Domain.Senior.PlayerSkillSet>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.PlayerSkillSet> builder)
        {
            builder.Property(p => p.Form)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Stamina)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Goalkeeping)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Defending)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Playmaking)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Winger)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Passing)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Scoring)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.SetPieces)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Experience)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Loyalty)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.PlayerSkillSet> builder)
        {
            builder.HasOne(m => m.Player)
                .WithMany(m => m.PlayerSkillSets)
                .HasConstraintName("FK_PlayerSkillSet_Player")
                .HasForeignKey(m => m.PlayerHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.PlayerSkillSet> builder)
        {
            builder.ToTable(Constants.TableName.PlayerSkillSet, Constants.Schema.Senior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Senior.PlayerSkillSet> builder)
        {
            builder.HasKey(p => new { p.PlayerHattrickId, p.Season, p.Week });

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Season)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Week)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }
    }
}