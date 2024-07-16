namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MatchReferee : AuditableEntityBase<Domain.Senior.MatchReferee>, IEntityTypeConfiguration<Domain.Senior.MatchReferee>, IEntityMapping<Domain.Senior.MatchReferee>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.MatchReferee> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.MatchReferee> builder)
        {
            builder.HasOne(m => m.Country)
                .WithMany(m => m.Referees)
                .HasConstraintName("FK_MatchReferee_Country")
                .HasForeignKey(m => m.CountryHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Match)
                .WithMany(m => m.Referees)
                .HasConstraintName("FK_MatchReferee_Match")
                .HasForeignKey(m => m.MatchHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.MatchReferee> builder)
        {
            builder.ToTable(Constants.TableName.MatchReferee, Constants.Schema.Senior);
        }

        protected override void MapBaseProperties(EntityTypeBuilder<Domain.Senior.MatchReferee> builder)
        {
            builder.HasKey(p => new { p.RefereeHattrickId, p.MatchHattrickId });

            builder.Property(p => p.RefereeHattrickId)
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