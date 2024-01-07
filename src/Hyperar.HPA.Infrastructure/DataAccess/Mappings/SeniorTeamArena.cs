namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SeniorTeamArena : HattrickEntityBase<Domain.SeniorTeamArena>, IEntityTypeConfiguration<Domain.SeniorTeamArena>, IEntityMapping<Domain.SeniorTeamArena>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.SeniorTeamArena> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.BuiltOn)
                .HasColumnName(Constants.ColumnName.BuiltOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.TerracesCapacity)
                .HasColumnName(Constants.ColumnName.TerracesCapacity)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.BasicSeatCapacity)
                .HasColumnName(Constants.ColumnName.BasicSeatCapacity)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.RoofSeatCapacity)
                .HasColumnName(Constants.ColumnName.RoofSeatCapacity)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.VipLoungeCapacity)
                .HasColumnName(Constants.ColumnName.VipLoungeCapacity)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.TotalCapacity)
                .HasColumnName(Constants.ColumnName.TotalCapacity)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.SeniorTeamArena> builder)
        {
            builder.ToTable(Constants.TableName.SeniorTeamArena);
        }
    }
}