using System.Web.Mvc;

namespace PersonalFinanceTracker.App_Start
{
    /// <summary>
    /// Filter Configuration
    /// Registers global action filters
    /// </summary>
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Handle errors globally
            filters.Add(new HandleErrorAttribute());

            // Note: Anti-forgery token validation is applied individually to POST actions
            // Do NOT add it globally as it will cause issues with GET requests
        }
    }
}
