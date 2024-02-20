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
                .HasColumnName(Constants.ColumnName.HattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasColumnName(Constants.ColumnName.FirstName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.NickName)
                .HasColumnName(Constants.ColumnName.NickName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(x => x.LastName)
                .HasColumnName(Constants.ColumnName.LastName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Role)
                .HasColumnName(Constants.ColumnName.Role)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();

            builder.Property(p => p.Behavior)
                .HasColumnName(Constants.ColumnName.Behavior)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.SmallInt);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpStartingPlayer> builder)
        {
            builder.HasOne(x => x.LineUp)
                .WithMany(x => x.StartingPlayers)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpStartingPlayer> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpStartingPlayer, Constants.Schema.Senior);
        }
    }
}