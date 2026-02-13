using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    /// <summary>
    /// ViewModel for budget management
    /// </summary>
    public class BudgetViewModel
    {
        public int BudgetId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Budget limit is required")]
        [Range(0.01, 1000000, ErrorMessage = "Budget must be between 0.01 and 1,000,000")]
        [Display(Name = "Monthly Limit")]
        public decimal LimitAmount { get; set; }

        [Required]
        [Range(1, 12)]
        [Display(Name = "Month")]
        public int Month { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }

        public string CategoryName { get; set; }

        // For dropdown population
        public List<Category> AvailableCategories { get; set; }

        public BudgetViewModel()
        {
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
            AvailableCategories = new List<Category>();
        }
    }

    /// <summary>
    /// ViewModel for displaying all budgets with progress
    /// </summary>
    public class BudgetListViewModel
    {
        public List<BudgetItemViewModel> Budgets { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalSpent { get; set; }

        public BudgetListViewModel()
        {
            Budgets = new List<BudgetItemViewModel>();
            CurrentMonth = DateTime.Now.Month;
            CurrentYear = DateTime.Now.Year;
        }
    }

    /// <summary>
    /// Individual budget item with spending details
    /// </summary>
    public class BudgetItemViewModel
    {
        public int BudgetId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string ColorCode { get; set; }
        public decimal LimitAmount { get; set; }
        public decimal AmountSpent { get; set; }
        public decimal Remaining { get; set; }
        public decimal PercentageUsed { get; set; }
        public bool IsOverBudget { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
