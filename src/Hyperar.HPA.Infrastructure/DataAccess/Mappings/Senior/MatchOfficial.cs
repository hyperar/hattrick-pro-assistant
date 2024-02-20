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
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchOfficial> builder)
        {
            builder.HasOne(m => m.Match)
                .WithMany(m => m.Officials)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Country)
                .WithMany();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchOfficial> builder)
        {
            builder.ToTable(Constants.TableName.MatchOfficial, Constants.Schema.Senior);
        }
    }
}