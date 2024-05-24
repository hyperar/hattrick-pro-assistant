namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TeamArena : HattrickEntityBase<Domain.Senior.TeamArena>, IEntityTypeConfiguration<Domain.Senior.TeamArena>, IEntityMapping<Domain.Senior.TeamArena>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.TeamArena> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.BuiltOn)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.TerracesCapacity)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.BasicSeatCapacity)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.RoofSeatCapacity)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.VipLoungeCapacity)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.TotalCapacity)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.ImageBytes)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.VarBinary);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.TeamArena> builder)
        {
            builder.HasOne(m => m.Team)
                .WithOne(m => m.TeamArena)
                .HasForeignKey<Domain.Senior.TeamArena>(m => m.TeamHattrickId)
                .HasConstraintName("FK_Senior_TeamArena_Team")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.TeamArena> builder)
        {
            builder.ToTable(Constants.TableName.TeamArena, Constants.Schema.Senior);
        }
    }
}