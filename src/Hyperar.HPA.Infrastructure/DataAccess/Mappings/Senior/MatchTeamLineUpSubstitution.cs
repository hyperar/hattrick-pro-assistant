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
                .HasColumnName(Constants.ColumnName.OrderType)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(x => x.NewRole)
                .HasColumnName(Constants.ColumnName.NewRole)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();

            builder.Property(x => x.NewRoleBehavior)
                .HasColumnName(Constants.ColumnName.NewRoleBehavior)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.SmallInt)
                .IsRequired();

            builder.Property(x => x.Minute)
                .HasColumnName(Constants.ColumnName.Minute)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnName(Constants.ColumnName.MatchPart)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
        {
            builder.HasOne(x => x.LineUp)
                .WithMany(x => x.Substitutions)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamLineUpSubstitution> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamLineUpSubstitution, Constants.Schema.Senior);
        }
    }
}