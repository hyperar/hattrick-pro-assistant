namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchTeamInjury : EntityBase<Domain.Senior.MatchTeamInjury>, IEntityTypeConfiguration<Domain.Senior.MatchTeamInjury>, IEntityMapping<Domain.Senior.MatchTeamInjury>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchTeamInjury> builder)
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

            builder.Property(p => p.Type)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Minute)
                .HasColumnOrder(5)
               .HasColumnType(Constants.ColumnType.TinyInt)
               .IsRequired();

            builder.Property(p => p.MatchPart)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchTeamInjury> builder)
        {
            builder.HasOne(m => m.MatchTeam)
                .WithMany(m => m.Injuries)
                .HasForeignKey(m => m.MatchTeamId)
                .HasConstraintName("FK_Senior_MatchTeamInjury_MatchTeam")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchTeamInjury> builder)
        {
            builder.ToTable(Constants.TableName.MatchTeamInjury, Constants.Schema.Senior);
        }
    }
}