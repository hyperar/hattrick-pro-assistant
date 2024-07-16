namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PlayerMatch : AuditableEntityBase<Domain.Junior.PlayerMatch>, IEntityTypeConfiguration<Domain.Junior.PlayerMatch>, IEntityMapping<Domain.Junior.PlayerMatch>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.PlayerMatch> builder)
        {
            builder.Property(p => p.Date)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.Role)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.AverageRating)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Decimal)
                .HasPrecision(5, 1)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.PlayerMatch> builder)
        {
            builder.HasOne(m => m.Match)
                .WithMany(m => m.PlayerMatches)
                .HasConstraintName("FK_PlayerMatch_Match")
                .HasForeignKey(m => m.MatchHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Player)
                .WithMany(m => m.PlayerMatches)
                .HasConstraintName("FK_PlayerMatch_Player")
                .HasForeignKey(m => m.PlayerHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.PlayerMatch> builder)
        {
            builder.ToTable(Constants.TableName.PlayerMatch, Constants.Schema.Junior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Junior.PlayerMatch> builder)
        {
            builder.HasKey(p => new { p.PlayerHattrickId, p.MatchHattrickId });

            builder.Property(p => p.PlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(p => p.MatchHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }
    }
}