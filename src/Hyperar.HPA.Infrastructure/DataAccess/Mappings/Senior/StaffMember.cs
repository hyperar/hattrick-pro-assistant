namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class StaffMember : HattrickEntityBase<Domain.Senior.StaffMember>, IEntityTypeConfiguration<Domain.Senior.StaffMember>, IEntityMapping<Domain.Senior.StaffMember>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Level)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.HiredOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.Salary)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.AvatarBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.StaffMembers)
                .HasConstraintName("FK_StaffMember_Team")
                .HasForeignKey(m => m.TeamHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.ToTable(Constants.TableName.StaffMember, Constants.Schema.Senior);
        }
    }
}