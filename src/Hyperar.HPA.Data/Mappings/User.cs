//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Hyperar">
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
    /// User Entity Type Configuration implementation.
    /// </summary>
    /// <typeparam name="TEntity">User class.</typeparam>
    internal class User : Entity<Domain.User>, IEntityMapping<Domain.User>
    {
        /// <summary>
        /// Maps the entity properties columns.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public override void MapProperties(EntityTypeBuilder<Domain.User> builder)
        {
            builder.Property(p => p.Token)
                .HasColumnName(Constants.ColumnName.Token)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.TokenSecret)
                .HasColumnName(Constants.ColumnName.TokenSecret)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.TokenCreatedOn)
                .HasColumnName(Constants.ColumnName.TokenCreatedOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();

            builder.Property(p => p.TokenExpiresOn)
                .HasColumnName(Constants.ColumnName.TokenExpiresOn)
                .HasColumnOrder(
                    this.GetCurrentColumnOrder())
                .HasColumnType(Constants.ColumnType.Date)
                .IsRequired();
        }

        /// <summary>
        /// Maps the entity relationships.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public override void MapRelationships(EntityTypeBuilder<Domain.User> builder)
        {
        }

        /// <summary>
        /// Maps the entity to a table.
        /// </summary>
        /// <param name="builder">The Entity Type Builder.</param>
        public override void MapTable(EntityTypeBuilder<Domain.User> builder)
        {
            builder.ToTable(Constants.TableName.User);
        }
    }
}