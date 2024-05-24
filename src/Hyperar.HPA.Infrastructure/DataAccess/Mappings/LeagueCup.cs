namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class LeagueCup : HattrickEntityBase<Domain.LeagueCup>, IEntityTypeConfiguration<Domain.LeagueCup>, IEntityMapping<Domain.LeagueCup>
    {
        public override sealed void MapProperties(EntityTypeBuilder<Domain.LeagueCup> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Type)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.SubType)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.SeriesLevel)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.CurrentRound)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.RoundsLeft)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override sealed void MapRelationships(EntityTypeBuilder<Domain.LeagueCup> builder)
        {
            builder.HasOne(m => m.League)
                .WithMany(m => m.Cups)
                .HasForeignKey(m => m.LeagueHattrickId)
                .HasConstraintName("FK_LeagueCup_League")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override sealed void MapTable(EntityTypeBuilder<Domain.LeagueCup> builder)
        {
            builder.ToTable(Constants.TableName.LeagueCup);
        }
    }
}