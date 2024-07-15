namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class LeagueCup : HattrickEntityBase<Domain.LeagueCup>, IEntityTypeConfiguration<Domain.LeagueCup>, IEntityMapping<Domain.LeagueCup>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.LeagueCup> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.SubType)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.Level)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.Week)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.WeeksLeft)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.LeagueCup> builder)
        {
            builder.HasOne(m => m.League)
                .WithMany(m => m.Cups)
                .HasConstraintName("FK_LeagueCup_League")
                .HasForeignKey(m => m.LeagueHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.LeagueCup> builder)
        {
            builder.ToTable(Constants.TableName.LeagueCup);
        }
    }
}