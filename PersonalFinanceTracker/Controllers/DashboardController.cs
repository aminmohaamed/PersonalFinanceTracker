using System;
using System.Web.Mvc;
using PersonalFinanceTracker.Filters;
using PersonalFinanceTracker.Services;
using PersonalFinanceTracker.ViewModels;

namespace PersonalFinanceTracker.Controllers
{
    /// <summary>
    /// Dashboard Controller
    /// Displays financial overview and summary
    /// </summary>
    [AuthorizeUser]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            try
            {
                var userId = GetCurrentUserId();
                var dashboardData = _dashboardService.GetDashboardData(userId);
                
                // Ensure model is never null
                if (dashboardData == null)
                {
                    dashboardData = new DashboardViewModel();
                }

                return View(dashboardData);
            }
            catch (Exception ex)
            {
                // Log error
                System.Diagnostics.Debug.WriteLine($"Dashboard Error: {ex.Message}");
                
                // Return empty model instead of error
                return View(new DashboardViewModel());
            }
        }

        /// <summary>
        /// AJAX endpoint to get chart data
        /// </summary>
        [HttpGet]
        public JsonResult GetChartData()
        {
            var userId = GetCurrentUserId();
            var dashboardData = _dashboardService.GetDashboardData(userId);

            var chartData = new
            {
                labels = dashboardData.ExpensesByCategory.ConvertAll(c => c.CategoryName),
                data = dashboardData.ExpensesByCategory.ConvertAll(c => c.Amount),
                colors = dashboardData.ExpensesByCategory.ConvertAll(c => c.ColorCode)
            };

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
    }
}
