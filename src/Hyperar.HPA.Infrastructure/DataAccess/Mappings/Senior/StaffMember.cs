namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class StaffMember : HattrickEntityBase<Domain.Senior.StaffMember>, IEntityTypeConfiguration<Domain.Senior.StaffMember>, IEntityMapping<Domain.Senior.StaffMember>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnName(Constants.ColumnName.Name)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.HiredOn)
                .HasColumnName(Constants.ColumnName.HiredOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName(Constants.ColumnName.Type)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(x => x.Level)
                .HasColumnName(Constants.ColumnName.Level)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsUnicode();

            builder.Property(p => p.Salary)
                .HasColumnName(Constants.ColumnName.Salary)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.AvatarBytes)
                .HasColumnName(Constants.ColumnName.AvatarBytes)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.HasOne(x => x.HallOfFamePlayer)
                .WithOne(x => x.Staff)
                .HasForeignKey<Domain.Senior.StaffMember>(x => x.HallOfFamePlayerId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.ToTable(Constants.TableName.StaffMember, Constants.Schema.Senior);
        }
    }
}