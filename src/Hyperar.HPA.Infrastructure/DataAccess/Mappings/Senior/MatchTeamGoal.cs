namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamGoal : EntityBase<Domain.Senior.MatchTeamGoal>, IEntityTypeConfiguration<Domain.Senior.MatchTeamGoal>, IEntityMapping<Domain.Senior.MatchTeamGoal>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamGoal> builder)
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

            builder.Property(p => p.HomeTeamScore)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.AwayTeamScore)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Minute)
                .HasColumnOrder(6)
               .HasColumnType(Constants.ColumnType.TinyInt)
               .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamGoal> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithMany(m => m.Goals)
                .HasForeignKey(m => m.MatchTeamId)
                .HasConstraintName("FK_Senior_MatchTeamGoal_MatchTeam")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamGoal> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamGoal, Constants.Schema.Senior);
        }
    }
}