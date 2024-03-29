﻿namespace Hyperar.HPA.Infrastructure.DataAccess
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IDatabaseContext context;

        public RepositoryBase(IDatabaseContext context)
        {
            this.context = context;
            this.EntityCollection = this.context.CreateSet<TEntity>();
        }

        protected DbSet<TEntity> EntityCollection { get; private set; }

        public async Task InsertAsync(TEntity entity)
        {
            await this.EntityCollection.AddAsync(entity);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return this.EntityCollection.Where(predicate ??= x => true);
        }

        public void Update(TEntity entity)
        {
            this.EntityCollection.Update(entity);
        }
    }
}