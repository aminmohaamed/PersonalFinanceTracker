using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    /// <summary>
    /// Represents a category for transactions (e.g., Food, Transport, Salary)
    /// </summary>
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public CategoryType Type { get; set; } // Income or Expense category

        [StringLength(7)]
        public string ColorCode { get; set; } // For chart visualization (e.g., #FF5733)

        [StringLength(50)]
        public string Icon { get; set; } // Icon class name (e.g., fa-utensils)

        // Navigation property
        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }

        public Category()
        {
            Transactions = new List<Transaction>();
            Budgets = new List<Budget>();
        }
    }

    /// <summary>
    /// Enum to define category types
    /// </summary>
    public enum CategoryType
    {
        Income = 1,
        Expense = 2,
        Both = 3
    }
}
