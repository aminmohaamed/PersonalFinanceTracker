using System.Web.Optimization;

namespace PersonalFinanceTracker.App_Start
{
    /// <summary>
    /// Bundle Configuration
    /// Manages CSS and JavaScript bundles for optimization
    /// </summary>
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Note: This application uses CDN references in _Layout.cshtml
            // Bundles are registered here for future extensibility if needed
            
            // If you want to use local files instead of CDN:
            // 1. Install NuGet packages: jQuery, Bootstrap, jQuery.Validation
            // 2. Uncomment the bundle configurations below
            
            // jQuery bundle (currently using CDN)
            // bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //             "~/Scripts/jquery-{version}.js"));

            // jQuery validation bundle (if needed)
            // bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //             "~/Scripts/jquery.validate*"));

            // Bootstrap bundle (currently using CDN)
            // bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //           "~/Scripts/bootstrap.js"));

            // CSS bundle (currently using CDN)
            // bundles.Add(new StyleBundle("~/Content/css").Include(
            //           "~/Content/bootstrap.css",
            //           "~/Content/site.css"));

            // Enable optimizations for production
            // Set to false for debugging
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
