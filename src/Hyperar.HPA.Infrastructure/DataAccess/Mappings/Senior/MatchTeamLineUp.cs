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
                .HasColumnName(Constants.ColumnName.Experience)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Style)
                .HasColumnName(Constants.ColumnName.Style)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
        {
            builder.HasOne(x => x.Team)
                .WithOne(x => x.LineUp)
                .HasForeignKey<Domain.Senior.MatchTeamLineUp>(m => m.TeamId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUp> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUp, Constants.Schema.Senior);
        }
    }
}