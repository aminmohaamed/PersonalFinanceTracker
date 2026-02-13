using System;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Repositories
{
    /// <summary>
    /// Unit of Work Interface
    /// Implements Unit of Work Pattern to manage transactions across multiple repositories
    /// Ensures data consistency and manages database context lifecycle
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Repository properties for each entity
        IRepository<User> Users { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<Category> Categories { get; }
        IRepository<Budget> Budgets { get; }

        /// <summary>
        /// Save all changes to the database
        /// </summary>
        int Complete();
    }

    /// <summary>
    /// Unit of Work Implementation
    /// Centralizes database operations and manages repository instances
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // Lazy initialization of repositories
        private IRepository<User> _users;
        private IRepository<Transaction> _transactions;
        private IRepository<Category> _categories;
        private IRepository<Budget> _budgets;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users
        {
            get { return _users ?? (_users = new Repository<User>(_context)); }
        }

        public IRepository<Transaction> Transactions
        {
            get { return _transactions ?? (_transactions = new Repository<Transaction>(_context)); }
        }

        public IRepository<Category> Categories
        {
            get { return _categories ?? (_categories = new Repository<Category>(_context)); }
        }

        public IRepository<Budget> Budgets
        {
            get { return _budgets ?? (_budgets = new Repository<Budget>(_context)); }
        }

        /// <summary>
        /// Commits all changes to the database
        /// Returns the number of affected rows
        /// </summary>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Dispose of database context
        /// Implements IDisposable pattern
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
