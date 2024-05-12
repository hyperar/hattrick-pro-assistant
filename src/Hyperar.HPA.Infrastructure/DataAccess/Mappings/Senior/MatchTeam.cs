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
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Location)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.MatchKitUrl)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(1024)
                .IsUnicode();

            builder.Property(p => p.MatchKitBytes)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.VarBinary);

            builder.Property(p => p.Formation)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(20);

            builder.Property(p => p.Score)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.TacticType)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.TacticLevel)
                .HasColumnOrder(9)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.MidfieldRating)
                .HasColumnOrder(10)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.RightDefenseRating)
                .HasColumnOrder(11)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.CentralDefenseRating)
                .HasColumnOrder(12)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.LeftDefenseRating)
                .HasColumnOrder(13)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.RightAttackRating)
                .HasColumnOrder(14)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.CentralAttackRating)
                .HasColumnOrder(15)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.LeftAttackRating)
                .HasColumnOrder(16)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.DefenseIndirectSetPiecesRating)
                .HasColumnOrder(17)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.AttackIndirectSetPiecesRating)
                .HasColumnOrder(18)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.Attitude)
                .HasColumnOrder(19)
                .HasColumnType(Constants.ColumnType.SmallInt);

            builder.Property(p => p.ChancesOnRight)
                .HasColumnOrder(20)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.ChancesOnCenter)
                .HasColumnOrder(21)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.ChancesOnLeft)
                .HasColumnOrder(22)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.SpecialEventChances)
                .HasColumnOrder(23)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.OtherChances)
                .HasColumnOrder(24)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.FirstHalfPosession)
                .HasColumnOrder(25)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.SecondHalfPosession)
                .HasColumnOrder(26)
                .HasColumnType(Constants.ColumnType.TinyInt);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeam> builder)
        {
            builder.HasOne(m => m.Match)
                .WithMany(m => m.Teams)
                .HasForeignKey(m => m.MatchHattrickId)
                .HasConstraintName("FK_Senior_MatchTeam_Match")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeam> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeam, Constants.Schema.Senior);
        }
    }
}