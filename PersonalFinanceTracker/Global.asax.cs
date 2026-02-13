using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Data.Entity;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.App_Start;

namespace PersonalFinanceTracker
{
    /// <summary>
    /// MVC Application Entry Point
    /// Handles application startup and initialization
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Register areas
            AreaRegistration.RegisterAllAreas();

            // Register routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register bundles (CSS/JS)
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Register filters
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Initialize database with seed data
            Database.SetInitializer(new DbInitializer());

            // Configure dependency injection
            DependencyConfig.RegisterDependencies();
        }

        /// <summary>
        /// Handle application errors
        /// </summary>
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            
            // Log the error (in production, use a logging framework like NLog or Serilog)
            System.Diagnostics.Debug.WriteLine($"Application Error: {exception?.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {exception?.StackTrace}");
            
            // Clear the error to prevent redirect loop
            Server.ClearError();
            
            // Note: Error handling is managed by customErrors in Web.config
            // Don't redirect here to avoid redirect loops
        }
    }
}
