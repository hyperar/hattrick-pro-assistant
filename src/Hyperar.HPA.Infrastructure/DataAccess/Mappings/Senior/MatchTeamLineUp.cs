namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamLineUp : EntityBase<Domain.Senior.MatchTeamLineUp>, IEntityTypeConfiguration<Domain.Senior.MatchTeamLineUp>, IEntityMapping<Domain.Senior.MatchTeamLineUp>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
        {
            builder.Property(p => p.Experience)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Style)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithOne(m => m.LineUp)
                .HasForeignKey<Domain.Senior.MatchTeamLineUp>(m => m.MatchTeamId)
                .HasConstraintName("FK_Senior_MatchTeamLineUp_MatchTeam")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUp, Constants.Schema.Senior);
        }
    }
}