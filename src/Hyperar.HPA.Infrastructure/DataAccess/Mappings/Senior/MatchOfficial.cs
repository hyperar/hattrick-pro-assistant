namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchOfficial : EntityBase<Domain.Senior.MatchOfficial>, IEntityTypeConfiguration<Domain.Senior.MatchOfficial>, IEntityMapping<Domain.Senior.MatchOfficial>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchOfficial> builder)
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
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchOfficial> builder)
        {
            builder.HasOne(m => m.Country)
                .WithMany(m => m.MatchOfficials)
                .HasForeignKey(m => m.CountryHattrickId)
                .HasConstraintName("FK_Senior_MatchOfficial_Country")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Match)
                .WithMany(m => m.Officials)
                .HasForeignKey(m => m.MatchHattrickId)
                .HasConstraintName("FK_Senior_MatchOfficial_Match")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchOfficial> builder)
        {
            builder.ToTable(Constants.TableName.MatchOfficial, Constants.Schema.Senior);
        }
    }
}