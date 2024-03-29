﻿namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System.Linq;
    using Domain;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class HattrickRepository<TEntity> : RepositoryBase<TEntity>, IRepositoryBase<TEntity>, IHattrickRepository<TEntity> where TEntity : HattrickEntityBase, IHattrickEntity
    {
        public HattrickRepository(IDatabaseContext context) : base(context)
        {
        }

        public async Task DeleteAsync(long hattrickId)
        {
            TEntity? entity = await this.GetByHattrickIdAsync(hattrickId);

            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            this.EntityCollection.Remove(entity);
        }

        public async Task<TEntity?> GetByHattrickIdAsync(long hattrickId)
        {
            return await this.EntityCollection.Where(x => x.HattrickId == hattrickId).SingleOrDefaultAsync();
        }
    }
}