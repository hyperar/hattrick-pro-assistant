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
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.HiredOn)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Level)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsUnicode();

            builder.Property(p => p.Salary)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.AvatarBytes)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.HasOne(m => m.HallOfFamePlayer)
                .WithOne(m => m.Staff)
                .HasForeignKey<Domain.Senior.StaffMember>(x => x.HallOfFamePlayerHattrickId)
                .HasConstraintName("FK_StaffMember_HallOfFamePlayer")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Team)
                .WithMany(m => m.StaffMembers)
                .HasForeignKey(m => m.TeamId)
                .HasConstraintName("FK_StaffMember_Team")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.StaffMember> builder)
        {
            builder.ToTable(Constants.TableName.StaffMember, Constants.Schema.Senior);
        }
    }
}