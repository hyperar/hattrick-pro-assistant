namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Junior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Team : HattrickEntityBase<Domain.Junior.Team>, IEntityTypeConfiguration<Domain.Junior.Team>, IEntityMapping<Domain.Junior.Team>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Junior.Team> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.ShortName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.FoundedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.CanBookFriendlyOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.TrainerPlayerHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Junior.Team> builder)
        {
            builder.HasOne(m => m.SeniorTeam)
                .WithOne(m => m.JuniorTeam)
                .HasConstraintName("FK_Team_Team")
                .HasForeignKey<Domain.Junior.Team>(m => m.SeniorTeamHattrickId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Junior.Team> builder)
        {
            builder.ToTable(Constants.TableName.Team, Constants.Schema.Junior);
        }
    }
}