﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Votus.Common.Domain;

namespace Votus.Common.Data
{
    public abstract class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDomainEntity
    {
        private readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public void Add(TEntity item)
        {
            DbSet.Add(item);

            _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public IEnumerable<TEntity> FindWhere(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(filter);
        }
    }
}
