using System;
using System.Collections.Generic;
using System.Linq;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Repositories;
using PersonalFinanceTracker.ViewModels;

namespace PersonalFinanceTracker.Services
{
    /// <summary>
    /// Transaction Service Interface
    /// Defines business logic operations for transactions
    /// </summary>
    public interface ITransactionService
    {
        Transaction GetById(int id, int userId);
        IEnumerable<Transaction> GetUserTransactions(int userId);
        IEnumerable<Transaction> GetTransactionsByDateRange(int userId, DateTime startDate, DateTime endDate);
        IEnumerable<Transaction> GetTransactionsByCategory(int userId, int categoryId);
        IEnumerable<Transaction> GetRecentTransactions(int userId, int count);
        bool CreateTransaction(TransactionViewModel model, int userId);
        bool UpdateTransaction(TransactionViewModel model, int userId);
        bool DeleteTransaction(int id, int userId);
        decimal GetTotalIncome(int userId, DateTime? startDate = null, DateTime? endDate = null);
        decimal GetTotalExpenses(int userId, DateTime? startDate = null, DateTime? endDate = null);
        List<CategorySummary> GetExpensesByCategory(int userId, DateTime? startDate = null, DateTime? endDate = null);
    }

    /// <summary>
    /// Transaction Service Implementation
    /// Implements business logic for transaction operations
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Transaction GetById(int id, int userId)
        {
            return _unitOfWork.Transactions.FirstOrDefault(t => t.TransactionId == id && t.UserId == userId);
        }

        public IEnumerable<Transaction> GetUserTransactions(int userId)
        {
            return _unitOfWork.Transactions.Find(t => t.UserId == userId).OrderByDescending(t => t.Date);
        }

        public IEnumerable<Transaction> GetTransactionsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Transactions
                .Find(t => t.UserId == userId && t.Date >= startDate && t.Date <= endDate)
                .OrderByDescending(t => t.Date);
        }

        public IEnumerable<Transaction> GetTransactionsByCategory(int userId, int categoryId)
        {
            return _unitOfWork.Transactions
                .Find(t => t.UserId == userId && t.CategoryId == categoryId)
                .OrderByDescending(t => t.Date);
        }

        public IEnumerable<Transaction> GetRecentTransactions(int userId, int count)
        {
            return _unitOfWork.Transactions
                .Find(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .Take(count);
        }

        public bool CreateTransaction(TransactionViewModel model, int userId)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = userId,
                    Description = model.Description,
                    Amount = model.Amount,
                    CategoryId = model.CategoryId,
                    Type = model.Type,
                    Date = model.Date,
                    CreatedDate = DateTime.Now
                };

                _unitOfWork.Transactions.Add(transaction);
                return _unitOfWork.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTransaction(TransactionViewModel model, int userId)
        {
            try
            {
                var transaction = GetById(model.TransactionId, userId);
                if (transaction == null) return false;

                transaction.Description = model.Description;
                transaction.Amount = model.Amount;
                transaction.CategoryId = model.CategoryId;
                transaction.Type = model.Type;
                transaction.Date = model.Date;

                _unitOfWork.Transactions.Update(transaction);
                return _unitOfWork.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteTransaction(int id, int userId)
        {
            try
            {
                var transaction = GetById(id, userId);
                if (transaction == null) return false;

                _unitOfWork.Transactions.Remove(transaction);
                return _unitOfWork.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public decimal GetTotalIncome(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _unitOfWork.Transactions.Find(t => t.UserId == userId && t.Type == TransactionType.Income);

            if (startDate.HasValue)
                query = query.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Date <= endDate.Value);

            return query.Sum(t => (decimal?)t.Amount) ?? 0;
        }

        public decimal GetTotalExpenses(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _unitOfWork.Transactions.Find(t => t.UserId == userId && t.Type == TransactionType.Expense);

            if (startDate.HasValue)
                query = query.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Date <= endDate.Value);

            return query.Sum(t => (decimal?)t.Amount) ?? 0;
        }

        public List<CategorySummary> GetExpensesByCategory(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _unitOfWork.Transactions.Find(t => t.UserId == userId && t.Type == TransactionType.Expense);

            if (startDate.HasValue)
                query = query.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.Date <= endDate.Value);

            var totalExpenses = query.Sum(t => (decimal?)t.Amount) ?? 0;

            var categorySummary = query
                .GroupBy(t => new { t.CategoryId, t.Category.Name, t.Category.ColorCode })
                .Select(g => new CategorySummary
                {
                    CategoryName = g.Key.Name,
                    Amount = g.Sum(t => t.Amount),
                    ColorCode = g.Key.ColorCode,
                    TransactionCount = g.Count(),
                    Percentage = totalExpenses > 0 ? (g.Sum(t => t.Amount) / totalExpenses * 100) : 0
                })
                .OrderByDescending(c => c.Amount)
                .ToList();

            return categorySummary;
        }
    }
}
