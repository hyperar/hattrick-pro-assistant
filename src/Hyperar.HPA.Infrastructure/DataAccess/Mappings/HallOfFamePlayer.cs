namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class HallOfFamePlayer : HattrickEntityBase<Domain.HallOfFamePlayer>, IEntityTypeConfiguration<Domain.HallOfFamePlayer>, IEntityMapping<Domain.HallOfFamePlayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.HallOfFamePlayer> builder)
        {
            builder.Property(x => x.FirstName)
                .HasColumnName(Constants.ColumnName.FirstName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.NickName)
                .HasColumnName(Constants.ColumnName.NickName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(x => x.LastName)
                .HasColumnName(Constants.ColumnName.LastName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Age)
                .HasColumnName(Constants.ColumnName.Age)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();

            builder.Property(x => x.JoinedTeamOn)
                .HasColumnName(Constants.ColumnName.JoinedTeamOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.NextBirthday)
                .HasColumnName(Constants.ColumnName.NextBirthday)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.IntroducedToHallOfFameOn)
                .HasColumnName(Constants.ColumnName.IntroducedToHallOfFameOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.ExpertType)
                .HasColumnName(Constants.ColumnName.ExpertType)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.HallOfFamePlayer> builder)
        {
            builder.HasOne(x => x.Country)
                .WithMany(x => x.HallOfFamePlayers);
        }

        public override void MapTable(EntityTypeBuilder<Domain.HallOfFamePlayer> builder)
        {
            builder.ToTable(Constants.TableName.HallOfFamePlayer);
        }
    }
}