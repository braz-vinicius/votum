using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Votus.Common.Domain;

namespace Votus.Common.Data
{
    public interface IRepository<TEntity> where TEntity : IDomainEntity
    {
        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);

        /// <summary>
        /// Get all elements of type {T} in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get all elements of type {T} in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> FindWhere(Expression<Func<TEntity, bool>> filter);

    }
}