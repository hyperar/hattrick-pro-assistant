namespace Hyperar.HPA.Infrastructure.DataAccess.Mappings.Senior
{
    using Infrastructure.DataAccess.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class HallOfFamePlayer : HattrickEntityBase<Domain.Senior.HallOfFamePlayer>, IEntityTypeConfiguration<Domain.Senior.HallOfFamePlayer>, IEntityMapping<Domain.Senior.HallOfFamePlayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Senior.HallOfFamePlayer> builder)
        {
            builder.Property(p => p.FirstName)
                .HasColumnOrder(1)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.NickName)
                .HasColumnOrder(2)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(p => p.LastName)
                .HasColumnOrder(3)
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Age)
                .HasColumnOrder(4)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.JoinedTeamOn)
                .HasColumnOrder(5)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.NextBirthday)
                .HasColumnOrder(6)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.IntroducedOn)
                .HasColumnOrder(7)
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.ExpertType)
                .HasColumnOrder(8)
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Senior.HallOfFamePlayer> builder)
        {
            builder.HasOne(m => m.Team)
                .WithMany(m => m.HallOfFamePlayers)
                .HasForeignKey(m => m.TeamHattrickId)
                .HasConstraintName("FK_Senior_HallOfFamePlayer_Team")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Country)
                .WithMany(m => m.HallOfFamePlayers)
                .HasForeignKey(m => m.CountryHattrickId)
                .HasConstraintName("FK_Senior_HallOfFamePlayer_Country")
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Senior.HallOfFamePlayer> builder)
        {
            builder.ToTable(Constants.TableName.HallOfFamePlayer, Constants.Schema.Senior);
        }
    }
}