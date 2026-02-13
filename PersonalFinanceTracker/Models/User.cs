using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    /// <summary>
    /// Represents a user in the system
    /// Uses Entity Framework for database mapping
    /// </summary>
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordHash { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation property - One user has many transactions
        public virtual ICollection<Transaction> Transactions { get; set; }

        // Navigation property - One user has many budgets
        public virtual ICollection<Budget> Budgets { get; set; }

        public User()
        {
            Transactions = new List<Transaction>();
            Budgets = new List<Budget>();
            CreatedDate = DateTime.Now;
        }
    }
}
