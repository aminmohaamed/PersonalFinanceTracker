using System.Web.Mvc;

namespace PersonalFinanceTracker.Controllers
{
    /// <summary>
    /// Base Controller
    /// Provides common functionality for all controllers
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Get current logged-in user ID from session
        /// </summary>
        protected int GetCurrentUserId()
        {
            if (Session["UserId"] != null)
            {
                return (int)Session["UserId"];
            }

            return 0;
        }

        /// <summary>
        /// Get current logged-in username from session
        /// </summary>
        protected string GetCurrentUsername()
        {
            if (Session["Username"] != null)
            {
                return Session["Username"].ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Check if user is authenticated
        /// </summary>
        protected bool IsAuthenticated()
        {
            return Session["UserId"] != null;
        }
    }
}
