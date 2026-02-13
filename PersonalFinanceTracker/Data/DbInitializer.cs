using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data
{
    /// <summary>
    /// Database initializer to seed initial data
    /// Implements the Strategy Pattern for database initialization
    /// </summary>
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            try
            {
                // Check if categories already exist
                if (!context.Categories.Any())
                {
                    // Seed default categories
                    var categories = GetDefaultCategories();
                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }

                base.Seed(context);
            }
            catch (Exception ex)
            {
                // Log error but don't throw to prevent application crash
                System.Diagnostics.Debug.WriteLine($"Database Seed Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns predefined categories for income and expenses
        /// </summary>
        private List<Category> GetDefaultCategories()
        {
            return new List<Category>
            {
                // Income Categories
                new Category { Name = "Salary", Description = "Monthly salary", Type = CategoryType.Income, ColorCode = "#28a745", Icon = "fa-money-bill-wave" },
                new Category { Name = "Freelance", Description = "Freelance work income", Type = CategoryType.Income, ColorCode = "#17a2b8", Icon = "fa-laptop-code" },
                new Category { Name = "Investments", Description = "Investment returns", Type = CategoryType.Income, ColorCode = "#6f42c1", Icon = "fa-chart-line" },
                new Category { Name = "Other Income", Description = "Miscellaneous income", Type = CategoryType.Income, ColorCode = "#20c997", Icon = "fa-plus-circle" },

                // Expense Categories
                new Category { Name = "Food & Dining", Description = "Groceries and restaurants", Type = CategoryType.Expense, ColorCode = "#fd7e14", Icon = "fa-utensils" },
                new Category { Name = "Transportation", Description = "Car, gas, public transport", Type = CategoryType.Expense, ColorCode = "#007bff", Icon = "fa-car" },
                new Category { Name = "Entertainment", Description = "Movies, games, hobbies", Type = CategoryType.Expense, ColorCode = "#e83e8c", Icon = "fa-gamepad" },
                new Category { Name = "Shopping", Description = "Clothing and personal items", Type = CategoryType.Expense, ColorCode = "#dc3545", Icon = "fa-shopping-bag" },
                new Category { Name = "Bills & Utilities", Description = "Electricity, water, internet", Type = CategoryType.Expense, ColorCode = "#ffc107", Icon = "fa-file-invoice-dollar" },
                new Category { Name = "Healthcare", Description = "Medical expenses", Type = CategoryType.Expense, ColorCode = "#6c757d", Icon = "fa-heartbeat" },
                new Category { Name = "Education", Description = "Courses, books, learning", Type = CategoryType.Expense, ColorCode = "#17a2b8", Icon = "fa-graduation-cap" },
                new Category { Name = "Rent", Description = "Monthly rent payment", Type = CategoryType.Expense, ColorCode = "#343a40", Icon = "fa-home" },
                new Category { Name = "Insurance", Description = "Health, car, life insurance", Type = CategoryType.Expense, ColorCode = "#6610f2", Icon = "fa-shield-alt" },
                new Category { Name = "Other Expenses", Description = "Miscellaneous expenses", Type = CategoryType.Expense, ColorCode = "#868e96", Icon = "fa-ellipsis-h" }
            };
        }
    }
}
