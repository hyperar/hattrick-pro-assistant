namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Match : HattrickEntityBase<Domain.Senior.Match>, IEntityTypeConfiguration<Domain.Senior.Match>, IEntityMapping<Domain.Senior.Match>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.Property(p => p.System)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.CompetitionId)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.BigInt);

            builder.Property(p => p.Rules)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.TinyInt)
                .IsRequired();

            builder.Property(p => p.StartDate)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.FinishDate)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.AddedMinutes)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.TinyInt);

            builder.Property(p => p.Weather)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.TinyInt);
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.Matches)
                .HasForeignKey(m => m.TeamHattrickId)
                .HasConstraintName("FK_Senior_Match_Team")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.Match> builder)
        {
            builder.ToTable(Constants.TableName.Match, Constants.Schema.Senior);
        }
    }
}