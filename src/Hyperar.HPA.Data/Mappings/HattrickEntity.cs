//-----------------------------------------------------------------------
// <copyright file="HattrickEntity.cs" company="Hyperar">
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
    /// Hattrick Entity Type Configuration base implementation.
    /// </summary>
    /// <typeparam name="TEntity">IEntity class.</typeparam>
    internal abstract class HattrickEntity<TEntity> : Entity<TEntity>, IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : Domain.HattrickEntityBase, Domain.Interfaces.IEntity, Domain.Interfaces.IHattrickEntity
    {
        /// <summary>
        /// Maps the common properties columns.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        protected override void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            base.MapBaseProperties(builder);

            builder.Property(p => p.HattrickId)
                .HasColumnName(Constants.ColumnName.HattrickId)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt)
                .IsRequired();
        }
    }
}