namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class StaffMemberAvatarLayer : EntityBase<Domain.StaffMemberAvatarLayer>, IEntityTypeConfiguration<Domain.StaffMemberAvatarLayer>, IEntityMapping<Domain.StaffMemberAvatarLayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.StaffMemberAvatarLayer> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.StaffMemberAvatarLayer> builder)
        {
            builder.HasOne(x => x.Staff)
                .WithMany(x => x.AvatarLayers)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.StaffMemberAvatarLayer> builder)
        {
            builder.ToTable(Constants.TableName.StaffMemberAvatarLayer);
        }
    }
}