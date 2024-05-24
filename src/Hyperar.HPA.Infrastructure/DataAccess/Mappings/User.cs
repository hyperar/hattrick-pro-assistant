namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class User : EntityBase<Domain.User>, IEntityTypeConfiguration<Domain.User>, IEntityMapping<Domain.User>
    {
        public override sealed void MapProperties(EntityTypeBuilder<Domain.User> builder)
        {
            builder.Property(p => p.LastDownloadDate)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.LastSelectedTeamHattrickId)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.BigInt);
        }

        public override sealed void MapTable(EntityTypeBuilder<Domain.User> builder)
        {
            builder.ToTable(Constants.TableName.User);
        }
    }
}