//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Hyperar">
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
    /// Entity Type Configuration base implementation.
    /// </summary>
    /// <typeparam name="TEntity">IEntity class.</typeparam>
    internal abstract class Entity<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : Domain.EntityBase, Domain.Interfaces.IEntity
    {
        /// <summary>
        /// Column order index.
        /// </summary>
        private int columnOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TEntity}" /> class.
        /// </summary>
        public Entity()
        {
            this.columnOrder = 0;
        }

        /// <summary>
        /// Entity mapping entry point.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.MapTable(builder);
            this.MapBaseProperties(builder);
            this.MapProperties(builder);
            this.MapRelationships(builder);
        }

        /// <summary>
        /// Maps the entity properties columns.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public abstract void MapProperties(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Maps the entity relationships.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public abstract void MapRelationships(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Maps the entity to a table.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public abstract void MapTable(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Advances the current column order and returns it.
        /// </summary>
        /// <returns>The current column order.</returns>
        protected int GetCurrentColumnOrder()
        {
            return this.columnOrder++;
        }

        /// <summary>
        /// Maps the common properties columns.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        protected virtual void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.Id)
                .HasColumnName(Constants.ColumnName.Id)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}