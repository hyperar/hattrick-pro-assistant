namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Token : EntityBase<Domain.Token>, IEntityTypeConfiguration<Domain.Token>, IEntityMapping<Domain.Token>
    {
        public override sealed void MapProperties(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.Property(p => p.Scope)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Value)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SecretValue)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.CreatedOn)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.ExpiresOn)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();
        }

        public override sealed void MapRelationships(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.HasOne(m => m.User)
                .WithOne(m => m.Token)
                .HasForeignKey<Domain.Token>(m => m.UserId)
                .HasConstraintName("FK_Token_User")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override sealed void MapTable(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.ToTable(Constants.TableName.Token);
        }
    }
}