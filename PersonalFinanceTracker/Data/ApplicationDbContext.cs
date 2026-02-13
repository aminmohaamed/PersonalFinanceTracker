using System.Data.Entity;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data
{
    /// <summary>
    /// Entity Framework Database Context
    /// Manages database connections and entity sets
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            // Enable lazy loading
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        // DbSets represent tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        /// <summary>
        /// Configure model relationships and constraints
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Transaction relationship
            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(true);

            // Configure Category-Transaction relationship
            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .WillCascadeOnDelete(false);

            // Configure User-Budget relationship
            modelBuilder.Entity<Budget>()
                .HasRequired(b => b.User)
                .WithMany(u => u.Budgets)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(true);

            // Configure Category-Budget relationship
            modelBuilder.Entity<Budget>()
                .HasRequired(b => b.Category)
                .WithMany(c => c.Budgets)
                .HasForeignKey(b => b.CategoryId)
                .WillCascadeOnDelete(false);

            // Ensure unique username
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Ensure unique email
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Ensure unique budget per category per month per user
            modelBuilder.Entity<Budget>()
                .HasIndex(b => new { b.UserId, b.CategoryId, b.Month, b.Year })
                .IsUnique();

            // Configure decimal precision for Amount
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            // Configure decimal precision for LimitAmount
            modelBuilder.Entity<Budget>()
                .Property(b => b.LimitAmount)
                .HasPrecision(18, 2);
        }
    }
}
