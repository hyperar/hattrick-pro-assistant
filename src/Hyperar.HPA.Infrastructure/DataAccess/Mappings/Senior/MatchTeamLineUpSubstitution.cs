namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUpSubstitution : EntityBase<Domain.Senior.MatchTeamLineUpSubstitution>, IEntityTypeConfiguration<Domain.Senior.MatchTeamLineUpSubstitution>, IEntityMapping<Domain.Senior.MatchTeamLineUpSubstitution>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
        {
            builder.Property(p => p.OrderType)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.NewRole)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.NewRoleBehavior)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Minute)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.InPlayerHattrickId)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.OutPlayerHattrickId)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
        {
            builder.HasOne(m => m.MatchTeamLineUp)
                .WithMany(m => m.Substitutions)
                .HasForeignKey(m => m.MatchTeamLineUpId)
                .HasConstraintName("FK_Senior_MatchTeamLineUpSubstitution_MatchTeamLineUp")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpSubstitution, Constants.Schema.Senior);
        }
    }
}