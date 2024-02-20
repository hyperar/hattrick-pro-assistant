namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeam : EntityBase<Domain.Senior.MatchTeam>, IEntityTypeConfiguration<Domain.Senior.MatchTeam>, IEntityMapping<Domain.Senior.MatchTeam>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeam> builder)
        {
            builder.Property(p => p.HattrickId)
                .HasColumnName(Constants.ColumnName.HattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.MatchKitUrl)
                .HasColumnName(Constants.ColumnName.MatchKitUrl)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(1024)
                .IsUnicode();

            builder.Property(p => p.MatchKitBytes)
                .HasColumnName(Constants.ColumnName.MatchKitBytes)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary);

            builder.Property(p => p.Formation)
                .HasColumnName(Constants.ColumnName.Formation)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20);

            builder.Property(p => p.Score)
                .HasColumnName(Constants.ColumnName.Score)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.TacticType)
                .HasColumnName(Constants.ColumnName.TacticType)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.TacticLevel)
                .HasColumnName(Constants.ColumnName.TacticLevel)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.MidfieldRating)
                .HasColumnName(Constants.ColumnName.MidfieldRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.RightDefenseRating)
                .HasColumnName(Constants.ColumnName.RightDefenseRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.CentralDefenseRating)
                .HasColumnName(Constants.ColumnName.CentralDefenseRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.LeftDefenseRating)
                .HasColumnName(Constants.ColumnName.LeftDefenseRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.RightAttackRating)
                .HasColumnName(Constants.ColumnName.RightAttackRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.CentralAttackRating)
                .HasColumnName(Constants.ColumnName.CentralAttackRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.LeftAttackRating)
                .HasColumnName(Constants.ColumnName.LeftAttackRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.DefenseIndirectSetPiecesRating)
                .HasColumnName(Constants.ColumnName.DefenseIndirectSetPiecesRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.AttackIndirectSetPiecesRating)
                .HasColumnName(Constants.ColumnName.AttackIndirectSetPiecesRating)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.Attitude)
                .HasColumnName(Constants.ColumnName.Attitude)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.SmallInt);

            builder.Property(p => p.ChancesOnRight)
                .HasColumnName(Constants.ColumnName.ChancesOnRight)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.ChancesOnCenter)
                .HasColumnName(Constants.ColumnName.ChancesOnCenter)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.ChancesOnLeft)
                .HasColumnName(Constants.ColumnName.ChancesOnLeft)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.SpecialEventChances)
                .HasColumnName(Constants.ColumnName.SpecialEventChances)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.OtherChances)
                .HasColumnName(Constants.ColumnName.OtherChances)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.FirstHalfPosession)
                .HasColumnName(Constants.ColumnName.FirstHalfPosession)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.SecondHalfPosession)
                .HasColumnName(Constants.ColumnName.SecondHalfPosession)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeam> builder)
        {
            builder.HasOne(m => m.Match)
                .WithMany(m => m.Teams)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeam> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeam, Constants.Schema.Senior);
        }
    }
}