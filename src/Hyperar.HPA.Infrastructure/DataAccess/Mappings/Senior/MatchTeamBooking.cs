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

            builder.Property(p => p.Type)
                .HasColumnName(Constants.ColumnName.Type)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamBooking> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.Bookings);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamBooking> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamBooking, Constants.Schema.Senior);
        }
    }
}