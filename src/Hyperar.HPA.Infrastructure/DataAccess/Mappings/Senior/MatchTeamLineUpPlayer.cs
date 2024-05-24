namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUpPlayer : EntityBase<Domain.Senior.MatchTeamLineUpPlayer>, IEntityTypeConfiguration<Domain.Senior.MatchTeamLineUpPlayer>, IEntityMapping<Domain.Senior.MatchTeamLineUpPlayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpPlayer> builder)
        {
            builder.Property(p => p.HattrickId)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.FirstName)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.NickName)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(p => p.LastName)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Role)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Behavior)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.Int);

            builder.Property(p => p.Rating)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(3, 1);

            builder.Property(p => p.EndRating)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(3, 1);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpPlayer> builder)
        {
            builder.HasOne(m => m.MatchTeamLineUp)
                .WithMany(m => m.Players)
                .HasForeignKey(m => m.MatchTeamLineUpId)
                .HasConstraintName("FK_Senior_MatchTeamLineUpPlayer_MatchTeamLineUp")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpPlayer> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpPlayer, Constants.Schema.Senior);
        }
    }
}