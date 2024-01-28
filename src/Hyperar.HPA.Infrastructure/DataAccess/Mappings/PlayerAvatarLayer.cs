namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PlayerAvatarLayer : EntityBase<Domain.PlayerAvatarLayer>, IEntityTypeConfiguration<Domain.PlayerAvatarLayer>, IEntityMapping<Domain.PlayerAvatarLayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.PlayerAvatarLayer> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.PlayerAvatarLayer> builder)
        {
            builder.HasOne(x => x.Player)
                .WithMany(x => x.AvatarLayers)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.PlayerAvatarLayer> builder)
        {
            builder.ToTable(Constants.TableName.PlayerAvatarLayer);
        }
    }
}