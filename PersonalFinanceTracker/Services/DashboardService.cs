using System;
using System.Linq;
using PersonalFinanceTracker.Repositories;
using PersonalFinanceTracker.ViewModels;

namespace PersonalFinanceTracker.Services
{
    /// <summary>
    /// Dashboard Service Interface
    /// Aggregates data for dashboard display
    /// </summary>
    public interface IDashboardService
    {
        DashboardViewModel GetDashboardData(int userId);
    }

    /// <summary>
    /// Dashboard Service Implementation
    /// Compiles data from multiple services for dashboard view
    /// </summary>
    public class DashboardService : IDashboardService
    {
        private readonly ITransactionService _transactionService;
        private readonly IBudgetService _budgetService;

        public DashboardService(ITransactionService transactionService, IBudgetService budgetService)
        {
            _transactionService = transactionService;
            _budgetService = budgetService;
        }

        public DashboardViewModel GetDashboardData(int userId)
        {
            // Get current month start and end dates
            var now = DateTime.Now;
            var monthStart = new DateTime(now.Year, now.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            // Calculate financial summary
            var monthlyIncome = _transactionService.GetTotalIncome(userId, monthStart, monthEnd);
            var monthlyExpenses = _transactionService.GetTotalExpenses(userId, monthStart, monthEnd);
            var totalIncome = _transactionService.GetTotalIncome(userId);
            var totalExpenses = _transactionService.GetTotalExpenses(userId);

            // Get recent transactions
            var recentTransactions = _transactionService.GetRecentTransactions(userId, 10).ToList();

            // Get expense breakdown by category
            var expensesByCategory = _transactionService.GetExpensesByCategory(userId, monthStart, monthEnd);

            // Get budget progress
            var budgetProgress = _budgetService.GetBudgetProgress(userId, now.Month, now.Year);

            return new DashboardViewModel
            {
                TotalBalance = totalIncome - totalExpenses,
                MonthlyIncome = monthlyIncome,
                MonthlyExpenses = monthlyExpenses,
                MonthlySavings = monthlyIncome - monthlyExpenses,
                RecentTransactions = recentTransactions,
                ExpensesByCategory = expensesByCategory,
                BudgetProgress = budgetProgress
            };
        }
    }
}
