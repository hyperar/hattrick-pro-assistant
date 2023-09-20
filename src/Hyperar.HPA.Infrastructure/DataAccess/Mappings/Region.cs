namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Region : HattrickEntityBase<Domain.Region>, IEntityTypeConfiguration<Domain.Region>, IEntityMapping<Domain.Region>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.HasOne(x => x.Country)
                .WithMany(x => x.Regions);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.ToTable(Constants.TableName.Region);
        }
    }
}