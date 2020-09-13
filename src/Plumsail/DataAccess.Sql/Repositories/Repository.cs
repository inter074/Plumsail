using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Sql.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        protected readonly DatabaseContext Context;
        protected readonly DatabaseFacade Database;

        protected Repository(DatabaseContext context)
        {
            Context = context;
            Database = context.Database;
        }

        public void AddOne(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.Entry(entity).State = EntityState.Added;

        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            entities = entities.ToList();
            Context.Set<TEntity>().AddRange(entities);
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
        }

        public void UpdateOne(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

        }

        public void UpdateMany(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Context.Set<TEntity>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;

            }
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> filter = null, bool? asNoTracking = null)
        {
            var query = filter != null ? Context.Set<TEntity>().Where(filter) : Context.Set<TEntity>();
            if (asNoTracking.HasValue && asNoTracking.Value)
                query = query.AsNoTracking();
            return query.FirstOrDefault();

        }

        public IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter != null ? Context.Set<TEntity>().Where(filter) : Context.Set<TEntity>();
            return query.ToArray();
        }
    }
}