using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    /// <summary>
    /// ViewModel for Dashboard display
    /// Aggregates all data needed for the dashboard view
    /// </summary>
    public class DashboardViewModel
    {
        public decimal TotalBalance { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal MonthlySavings { get; set; }

        public List<Transaction> RecentTransactions { get; set; }
        public List<CategorySummary> ExpensesByCategory { get; set; }
        public List<BudgetProgress> BudgetProgress { get; set; }

        public DashboardViewModel()
        {
            RecentTransactions = new List<Transaction>();
            ExpensesByCategory = new List<CategorySummary>();
            BudgetProgress = new List<BudgetProgress>();
        }
    }

    /// <summary>
    /// Summary of transactions by category (for charts)
    /// </summary>
    public class CategorySummary
    {
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public string ColorCode { get; set; }
        public int TransactionCount { get; set; }
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Budget progress tracking
    /// </summary>
    public class BudgetProgress
    {
        public string CategoryName { get; set; }
        public decimal BudgetLimit { get; set; }
        public decimal AmountSpent { get; set; }
        public decimal Remaining { get; set; }
        public decimal PercentageUsed { get; set; }
        public string ColorCode { get; set; }
        public bool IsOverBudget { get; set; }
    }
}
