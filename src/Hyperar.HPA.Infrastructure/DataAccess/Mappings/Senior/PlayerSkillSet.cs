namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PlayerSkillSet : EntityBase<Domain.Senior.PlayerSkillSet>, IEntityTypeConfiguration<Domain.Senior.PlayerSkillSet>, IEntityMapping<Domain.Senior.PlayerSkillSet>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.PlayerSkillSet> builder)
        {
            builder.Property(p => p.Season)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();

            builder.Property(p => p.Week)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Form)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Stamina)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
            builder.Property(p => p.Keeper)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Defending)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Playmaking)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Winger)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Passing)
                .HasColumnOrder(9)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Scoring)
                .HasColumnOrder(10)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.SetPieces)
                .HasColumnOrder(11)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Loyalty)
                .HasColumnOrder(12)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Experience)
                .HasColumnOrder(13)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.PlayerSkillSet> builder)
        {
            builder.HasOne(m => m.Player)
                .WithMany(m => m.PlayerSkillSets)
                .HasForeignKey(m => m.PlayerHattrickId)
                .HasConstraintName("FK_PlayerSkillSet_Player")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.PlayerSkillSet> builder)
        {
            builder.ToTable(Constants.TableName.PlayerSkillSet, Constants.Schema.Senior);
        }
    }
}