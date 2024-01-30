namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class StaffMember : HattrickEntityBase<Domain.StaffMember>, IEntityTypeConfiguration<Domain.StaffMember>, IEntityMapping<Domain.StaffMember>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.StaffMember> builder)
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
                .HasColumnType(Constants.ColumnType.BigInt)
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

            builder.Property(p => p.Avatar)
                .HasColumnName(Constants.ColumnName.Avatar)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.StaffMember> builder)
        {
            builder.HasOne(x => x.HallOfFamePlayer)
                .WithOne(x => x.Staff)
                .HasForeignKey<Domain.StaffMember>(x => x.HallOfFamePlayerId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.StaffMember> builder)
        {
            builder.ToTable(Constants.TableName.StaffMember);
        }
    }
}