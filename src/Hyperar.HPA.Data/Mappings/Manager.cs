namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Manager : HattrickEntity<Domain.Database.Manager>, IEntityMapping<Domain.Database.Manager>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Database.Manager> builder)
        {
            builder.Property(p => p.UserName)
                .HasColumnName(Constants.ColumnName.UserName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SupporterTier)
                .HasColumnName(Constants.ColumnName.SupporterTier)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Database.Manager> builder)
        {
        }

        public override void MapTable(EntityTypeBuilder<Domain.Database.Manager> builder)
        {
            builder.ToTable(Constants.TableName.Manager);
        }
    }
}