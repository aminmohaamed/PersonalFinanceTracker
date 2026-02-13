using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models
{
    /// <summary>
    /// Represents a financial transaction (income or expense)
    /// </summary>
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public TransactionType Type { get; set; } // Income or Expense

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public Transaction()
        {
            CreatedDate = DateTime.Now;
            Date = DateTime.Today;
        }
    }

    /// <summary>
    /// Enum to define transaction types
    /// </summary>
    public enum TransactionType
    {
        Income = 1,
        Expense = 2
    }
}
