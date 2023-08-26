namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Token : Entity<Domain.Database.Token>, IEntityMapping<Domain.Database.Token>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Database.Token> builder)
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

        public override void MapRelationships(EntityTypeBuilder<Domain.Database.Token> builder)
        {
        }

        public override void MapTable(EntityTypeBuilder<Domain.Database.Token> builder)
        {
            builder.ToTable(Constants.TableName.Token);
        }
    }
}