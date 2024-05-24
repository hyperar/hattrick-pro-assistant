namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Region : HattrickEntityBase<Domain.Region>, IEntityTypeConfiguration<Domain.Region>, IEntityMapping<Domain.Region>
    {
        public override sealed void MapProperties(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();
        }

        public override sealed void MapRelationships(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.HasOne(m => m.Country)
                .WithMany(m => m.Regions)
                .HasForeignKey(m => m.CountryHattrickId)
                .HasConstraintName("FK_Region_Country")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override sealed void MapTable(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.ToTable(Constants.TableName.Region);
        }
    }
}