namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Hyperar.HPA.Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class User : EntityBase<Domain.User>, IEntityTypeConfiguration<Domain.User>, IEntityMapping<Domain.User>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.User> builder)
        {
            builder.Property(p => p.LastDownloadDate)
                .HasColumnName(Constants.ColumnName.LastDownloadDate)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.User> builder)
        {
            builder.HasOne(x => x.Token)
                .WithOne(x => x.User)
                .HasForeignKey<Domain.Token>(x => x.UserId);

            builder.HasOne(x => x.Manager)
                .WithOne(x => x.User)
                .HasForeignKey<Domain.Manager>(x => x.UserId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.User> builder)
        {
            builder.ToTable(Constants.TableName.User);
        }
    }
}