using System;
using System.Collections.Generic;
using System.Linq;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Repositories;
using PersonalFinanceTracker.ViewModels;

namespace PersonalFinanceTracker.Services
{
    /// <summary>
    /// Budget Service Interface
    /// Defines business logic operations for budgets
    /// </summary>
    public interface IBudgetService
    {
        Budget GetById(int id, int userId);
        IEnumerable<Budget> GetUserBudgets(int userId, int? month = null, int? year = null);
        bool CreateBudget(BudgetViewModel model, int userId);
        bool UpdateBudget(BudgetViewModel model, int userId);
        bool DeleteBudget(int id, int userId);
        List<BudgetProgress> GetBudgetProgress(int userId, int month, int year);
        BudgetListViewModel GetBudgetListViewModel(int userId, int month, int year);
    }

    /// <summary>
    /// Budget Service Implementation
    /// Implements business logic for budget operations
    /// </summary>
    public class BudgetService : IBudgetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;

        public BudgetService(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
        }

        public Budget GetById(int id, int userId)
        {
            return _unitOfWork.Budgets.FirstOrDefault(b => b.BudgetId == id && b.UserId == userId);
        }

        public IEnumerable<Budget> GetUserBudgets(int userId, int? month = null, int? year = null)
        {
            var query = _unitOfWork.Budgets.Find(b => b.UserId == userId);

            if (month.HasValue)
                query = query.Where(b => b.Month == month.Value);

            if (year.HasValue)
                query = query.Where(b => b.Year == year.Value);

            return query.ToList();
        }

        public bool CreateBudget(BudgetViewModel model, int userId)
        {
            try
            {
                // Check if budget already exists for this category/month/year
                var existing = _unitOfWork.Budgets.FirstOrDefault(b =>
                    b.UserId == userId &&
                    b.CategoryId == model.CategoryId &&
                    b.Month == model.Month &&
                    b.Year == model.Year);

                if (existing != null) return false;

                var budget = new Budget
                {
                    UserId = userId,
                    CategoryId = model.CategoryId,
                    LimitAmount = model.LimitAmount,
                    Month = model.Month,
                    Year = model.Year,
                    CreatedDate = DateTime.Now
                };

                _unitOfWork.Budgets.Add(budget);
                return _unitOfWork.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateBudget(BudgetViewModel model, int userId)
        {
            try
            {
                var budget = GetById(model.BudgetId, userId);
                if (budget == null) return false;

                budget.CategoryId = model.CategoryId;
                budget.LimitAmount = model.LimitAmount;
                budget.Month = model.Month;
                budget.Year = model.Year;

                _unitOfWork.Budgets.Update(budget);
                return _unitOfWork.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBudget(int id, int userId)
        {
            try
            {
                var budget = GetById(id, userId);
                if (budget == null) return false;

                _unitOfWork.Budgets.Remove(budget);
                return _unitOfWork.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public List<BudgetProgress> GetBudgetProgress(int userId, int month, int year)
        {
            var budgets = GetUserBudgets(userId, month, year).ToList();
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var budgetProgress = new List<BudgetProgress>();

            foreach (var budget in budgets)
            {
                var spent = _transactionService.GetTransactionsByCategory(userId, budget.CategoryId)
                    .Where(t => t.Type == TransactionType.Expense && 
                                t.Date >= startDate && 
                                t.Date <= endDate)
                    .Sum(t => (decimal?)t.Amount) ?? 0;

                var remaining = budget.LimitAmount - spent;
                var percentageUsed = budget.LimitAmount > 0 ? (spent / budget.LimitAmount * 100) : 0;

                budgetProgress.Add(new BudgetProgress
                {
                    CategoryName = budget.Category.Name,
                    BudgetLimit = budget.LimitAmount,
                    AmountSpent = spent,
                    Remaining = remaining,
                    PercentageUsed = percentageUsed,
                    ColorCode = budget.Category.ColorCode,
                    IsOverBudget = spent > budget.LimitAmount
                });
            }

            return budgetProgress.OrderByDescending(b => b.PercentageUsed).ToList();
        }

        public BudgetListViewModel GetBudgetListViewModel(int userId, int month, int year)
        {
            var budgets = GetUserBudgets(userId, month, year).ToList();
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var budgetItems = new List<BudgetItemViewModel>();

            foreach (var budget in budgets)
            {
                var spent = _transactionService.GetTransactionsByCategory(userId, budget.CategoryId)
                    .Where(t => t.Type == TransactionType.Expense && 
                                t.Date >= startDate && 
                                t.Date <= endDate)
                    .Sum(t => (decimal?)t.Amount) ?? 0;

                var remaining = budget.LimitAmount - spent;
                var percentageUsed = budget.LimitAmount > 0 ? (spent / budget.LimitAmount * 100) : 0;

                budgetItems.Add(new BudgetItemViewModel
                {
                    BudgetId = budget.BudgetId,
                    CategoryName = budget.Category.Name,
                    CategoryIcon = budget.Category.Icon,
                    ColorCode = budget.Category.ColorCode,
                    LimitAmount = budget.LimitAmount,
                    AmountSpent = spent,
                    Remaining = remaining,
                    PercentageUsed = percentageUsed,
                    IsOverBudget = spent > budget.LimitAmount,
                    Month = budget.Month,
                    Year = budget.Year
                });
            }

            return new BudgetListViewModel
            {
                Budgets = budgetItems.OrderByDescending(b => b.PercentageUsed).ToList(),
                CurrentMonth = month,
                CurrentYear = year,
                TotalBudget = budgetItems.Sum(b => b.LimitAmount),
                TotalSpent = budgetItems.Sum(b => b.AmountSpent)
            };
        }
    }
}
