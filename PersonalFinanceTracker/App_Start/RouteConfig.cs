using System.Web.Mvc;
using System.Web.Routing;

namespace PersonalFinanceTracker.App_Start
{
    /// <summary>
    /// Route Configuration
    /// Defines URL routing patterns for the application
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore route for .axd files (used by ASP.NET infrastructure)
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Default route pattern
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            // Additional custom routes can be added here
            // Example: Dashboard as default after login
            routes.MapRoute(
                name: "Dashboard",
                url: "Dashboard",
                defaults: new { controller = "Dashboard", action = "Index" }
            );
        }
    }
}
