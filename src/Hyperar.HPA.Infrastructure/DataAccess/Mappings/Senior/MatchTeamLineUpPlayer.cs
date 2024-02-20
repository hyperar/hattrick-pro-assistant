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

            builder.Property(p => p.Rating)
                .HasColumnName(Constants.ColumnName.Rating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Decimal);

            builder.Property(p => p.EndRating)
                .HasColumnName(Constants.ColumnName.EndRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Decimal);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpPlayer> builder)
        {
            builder.HasOne(m => m.LineUp)
                .WithMany(m => m.Players)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpPlayer> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpPlayer, Constants.Schema.Senior);
        }
    }
}