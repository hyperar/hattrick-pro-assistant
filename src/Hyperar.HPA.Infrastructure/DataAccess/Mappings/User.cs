namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class User : EntityBase<Domain.User>, IEntityTypeConfiguration<Domain.User>, IEntityMapping<Domain.User>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.User> builder)
        {
            builder.Property(p => p.LastDownloadDate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.SelectedTeamHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);
        }

        public override void MapTable(EntityTypeBuilder<Domain.User> builder)
        {
            builder.ToTable(Constants.TableName.User);
        }
    }
}