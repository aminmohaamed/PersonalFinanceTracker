using System.Web.Mvc;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Repositories;
using PersonalFinanceTracker.Services;

namespace PersonalFinanceTracker.App_Start
{
    /// <summary>
    /// Dependency Injection Configuration
    /// Implements Service Locator Pattern (simplified DI)
    /// In production, consider using a DI container like Unity, Autofac, or Ninject
    /// </summary>
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            // Register custom dependency resolver
            DependencyResolver.SetResolver(new CustomDependencyResolver());
        }
    }

    /// <summary>
    /// Custom Dependency Resolver
    /// Resolves dependencies for controllers and services
    /// </summary>
    public class CustomDependencyResolver : IDependencyResolver
    {
        public object GetService(System.Type serviceType)
        {
            // Resolve controllers
            if (serviceType == typeof(Controllers.AccountController))
            {
                var unitOfWork = new UnitOfWork(new ApplicationDbContext());
                var authService = new AuthService(unitOfWork);
                return new Controllers.AccountController(authService);
            }

            if (serviceType == typeof(Controllers.DashboardController))
            {
                var unitOfWork = new UnitOfWork(new ApplicationDbContext());
                var transactionService = new TransactionService(unitOfWork);
                var budgetService = new BudgetService(unitOfWork, transactionService);
                var dashboardService = new DashboardService(transactionService, budgetService);
                return new Controllers.DashboardController(dashboardService);
            }

            if (serviceType == typeof(Controllers.TransactionController))
            {
                var unitOfWork = new UnitOfWork(new ApplicationDbContext());
                var transactionService = new TransactionService(unitOfWork);
                return new Controllers.TransactionController(transactionService, unitOfWork);
            }

            if (serviceType == typeof(Controllers.BudgetController))
            {
                var unitOfWork = new UnitOfWork(new ApplicationDbContext());
                var transactionService = new TransactionService(unitOfWork);
                var budgetService = new BudgetService(unitOfWork, transactionService);
                return new Controllers.BudgetController(budgetService, unitOfWork);
            }

            // Default resolution
            return null;
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(System.Type serviceType)
        {
            return new System.Collections.Generic.List<object>();
        }
    }
}
