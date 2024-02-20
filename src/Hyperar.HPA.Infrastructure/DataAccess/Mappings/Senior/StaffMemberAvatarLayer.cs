namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class StaffMemberAvatarLayer : EntityBase<Domain.Senior.StaffMemberAvatarLayer>, IEntityTypeConfiguration<Domain.Senior.StaffMemberAvatarLayer>, IEntityMapping<Domain.Senior.StaffMemberAvatarLayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.StaffMemberAvatarLayer> builder)
        {
            builder.Property(p => p.Index)
                .HasColumnName(Constants.ColumnName.Index)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.XCoordinate)
                .HasColumnName(Constants.ColumnName.XCoordinate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.YCoordinate)
                .HasColumnName(Constants.ColumnName.YCoordinate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .HasColumnName(Constants.ColumnName.ImageUrl)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(1024)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.StaffMemberAvatarLayer> builder)
        {
            builder.HasOne(x => x.Staff)
                .WithMany(x => x.AvatarLayers);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.StaffMemberAvatarLayer> builder)
        {
            builder.ToTable(Constants.TableName.StaffMemberAvatarLayer, Constants.Schema.Senior);
        }
    }
}