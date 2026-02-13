using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PersonalFinanceTracker.Repositories
{
    /// <summary>
    /// Generic Repository Interface
    /// Implements Repository Pattern for data access abstraction
    /// Provides CRUD operations and common query methods
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get entity by ID
        /// </summary>
        T GetById(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Find entities matching a condition
        /// </summary>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get first entity matching a condition or null
        /// </summary>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add a new entity
        /// </summary>
        void Add(T entity);

        /// <summary>
        /// Add multiple entities
        /// </summary>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Update an existing entity
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Remove an entity
        /// </summary>
        void Remove(T entity);

        /// <summary>
        /// Remove multiple entities
        /// </summary>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// Check if any entity matches the condition
        /// </summary>
        bool Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Count entities matching a condition
        /// </summary>
        int Count(Expression<Func<T, bool>> predicate);
    }
}
