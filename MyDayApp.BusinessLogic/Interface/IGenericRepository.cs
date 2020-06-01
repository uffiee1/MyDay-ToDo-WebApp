﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyDayApp.DataAccess.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        void Insert(TEntity entityToInsert);
        void Update(TEntity entityToUpdate);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        TEntity GetById(object id);
        IEnumerable<TEntity> 
            Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includedProperties = ""
            );
    }
}