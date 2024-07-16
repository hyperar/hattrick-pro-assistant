namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Token : EntityBase<Domain.Token>, IEntityTypeConfiguration<Domain.Token>, IEntityMapping<Domain.Token>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.Property(p => p.Value)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(p => p.Secret)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(p => p.Scope)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.GeneratedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.ExpiresOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.HasOne(m => m.User)
                .WithOne(m => m.Token)
                .HasConstraintName("FK_Token_User")
                .HasForeignKey<Domain.Token>(m => m.UserId)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.ToTable(Constants.TableName.Token);
        }
    }
}