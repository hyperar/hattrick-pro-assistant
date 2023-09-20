namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Token : EntityBase<Domain.Token>, IEntityTypeConfiguration<Domain.Token>, IEntityMapping<Domain.Token>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Token> builder)
        {
            builder.Property(p => p.TokenValue)
                .HasColumnName(Constants.ColumnName.TokenValue)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.TokenSecretValue)
                .HasColumnName(Constants.ColumnName.TokenSecretValue)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.TokenCreatedOn)
                .HasColumnName(Constants.ColumnName.TokenCreatedOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(p => p.TokenExpiresOn)
                .HasColumnName(Constants.ColumnName.TokenExpiresOn)
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