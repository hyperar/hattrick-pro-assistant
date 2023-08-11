//-----------------------------------------------------------------------
// <copyright file="IEntityMapping.cs" company="Hyperar">
//     Copyright (c) Hyperar. All rights reserved.
// </copyright>
// <author>Matías Ezequiel Sánchez</author>
//-----------------------------------------------------------------------
namespace Hyperar.HPA.DataContracts
{
    using Hyperar.HPA.Domain;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Entity mapping contract.
    /// </summary>
    public interface IEntityMapping<TEntity> where TEntity : EntityBase, IEntity
    {
        /// <summary>
        /// Maps the entity properties columns.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        void MapProperties(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Maps the entity relationships.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        void MapRelationships(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Maps the entity to a table.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        void MapTable(EntityTypeBuilder<TEntity> builder);
    }
}