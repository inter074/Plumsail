﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        void AddOne(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);

        void UpdateOne(TEntity entity);
        void UpdateMany(IEnumerable<TEntity> entities);

        TEntity FindOne(Expression<Func<TEntity, bool>> filter = null, bool? asNoTracking = null);
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> filter = null);
    }

}