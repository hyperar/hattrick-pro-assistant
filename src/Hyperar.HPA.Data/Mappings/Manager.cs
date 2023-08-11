//-----------------------------------------------------------------------
// <copyright file="Manager.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.Data.Mappings
{
    using Hyperar.HPA.DataContracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Manager Entity Type Configuration implementation.
    /// </summary>
    /// <typeparam name="TEntity">Manager class.</typeparam>
    internal class Manager : HattrickEntity<Domain.Manager>, IEntityMapping<Domain.Manager>
    {
        /// <summary>
        /// Maps the entity properties columns.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public override void MapProperties(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.Property(p => p.UserName)
                .HasColumnName(Constants.ColumnName.UserName)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.SupporterTier)
                .HasColumnName(Constants.ColumnName.SupporterTier)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        /// <summary>
        /// Maps the entity relationships.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public override void MapRelationships(EntityTypeBuilder<Domain.Manager> builder)
        {
        }

        /// <summary>
        /// Maps the entity to a table.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public override void MapTable(EntityTypeBuilder<Domain.Manager> builder)
        {
            builder.ToTable(Constants.TableName.Manager);
        }
    }
}