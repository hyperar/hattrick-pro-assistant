namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUpStartingPlayer : EntityBase<Domain.Senior.MatchTeamLineUpStartingPlayer>, IEntityTypeConfiguration<Domain.Senior.MatchTeamLineUpStartingPlayer>, IEntityMapping<Domain.Senior.MatchTeamLineUpStartingPlayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpStartingPlayer> builder)
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
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpStartingPlayer> builder)
        {
            builder.HasOne(m => m.MatchTeamLineUp)
                .WithMany(m => m.StartingPlayers)
                .HasForeignKey(m => m.MatchTeamLineUpId)
                .HasConstraintName("FK_Senior_MatchTeamLineUpStartingPlayer_MatchTeamLineUp")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpStartingPlayer> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpStartingPlayer, Constants.Schema.Senior);
        }
    }
}