using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    /// <summary>
    /// ViewModel for adding/editing transactions
    /// </summary>
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Transaction type is required")]
        [Display(Name = "Type")]
        public TransactionType Type { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        // For dropdown population
        public List<Category> AvailableCategories { get; set; }

        public TransactionViewModel()
        {
            Date = DateTime.Today;
            AvailableCategories = new List<Category>();
        }
    }

    /// <summary>
    /// ViewModel for transaction history with filtering
    /// </summary>
    public class TransactionHistoryViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public List<Category> Categories { get; set; }

        // Filter properties
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CategoryId { get; set; }
        public TransactionType? Type { get; set; }

        public TransactionHistoryViewModel()
        {
            Transactions = new List<Transaction>();
            Categories = new List<Category>();
        }
    }
}
