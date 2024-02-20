namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PlayerAvatarLayer : EntityBase<Domain.Senior.PlayerAvatarLayer>, IEntityTypeConfiguration<Domain.Senior.PlayerAvatarLayer>, IEntityMapping<Domain.Senior.PlayerAvatarLayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.PlayerAvatarLayer> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.PlayerAvatarLayer> builder)
        {
            builder.HasOne(x => x.Player)
                .WithMany(x => x.AvatarLayers);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.PlayerAvatarLayer> builder)
        {
            builder.ToTable(Constants.TableName.PlayerAvatarLayer, Constants.Schema.Senior);
        }
    }
}