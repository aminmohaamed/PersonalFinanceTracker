using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models
{
    /// <summary>
    /// Represents a budget limit for a specific category
    /// </summary>
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Budget limit is required")]
        [Range(0.01, 1000000, ErrorMessage = "Budget must be greater than 0")]
        public decimal LimitAmount { get; set; }

        [Required]
        public int Month { get; set; } // 1-12

        [Required]
        public int Year { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public Budget()
        {
            CreatedDate = DateTime.Now;
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
        }
    }
}
