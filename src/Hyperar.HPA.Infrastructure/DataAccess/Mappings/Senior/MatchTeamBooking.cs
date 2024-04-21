namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamBooking : EntityBase<Domain.Senior.MatchTeamBooking>, IEntityTypeConfiguration<Domain.Senior.MatchTeamBooking>, IEntityMapping<Domain.Senior.MatchTeamBooking>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamBooking> builder)
        {
            builder.Property(p => p.Index)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.PlayerName)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Minute)
                .HasColumnOrder(5)
               .HasColumnType(Constants.ColumnType.TinyInt)
               .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamBooking> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithMany(m => m.Bookings)
                .HasForeignKey(m => m.MatchTeamId)
                .HasConstraintName("FK_Senior_MatchTeamBooking_MatchTeam")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamBooking> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamBooking, Constants.Schema.Senior);
        }
    }
}