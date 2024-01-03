namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Token : EntityBase<Domain.Token>, IEntityTypeConfiguration<Domain.Token>, IEntityMapping<Domain.Token>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.Property(p => p.Value)
                .HasColumnName(Constants.ColumnName.Value)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SecretValue)
                .HasColumnName(Constants.ColumnName.SecretValue)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.CreatedOn)
                .HasColumnName(Constants.ColumnName.CreatedOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(p => p.ExpiresOn)
                .HasColumnName(Constants.ColumnName.ExpiresOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.ToTable(Constants.TableName.Token);
        }
    }
}