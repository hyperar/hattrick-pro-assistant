namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Region : HattrickEntity<Domain.Database.Region>, IEntityMapping<Domain.Database.Region>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Database.Region> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.Database.Region> builder)
        {
            builder.HasOne(x => x.Country)
                .WithMany(x => x.Regions);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Database.Region> builder)
        {
            builder.ToTable(Constants.TableName.Region);
        }
    }
}
