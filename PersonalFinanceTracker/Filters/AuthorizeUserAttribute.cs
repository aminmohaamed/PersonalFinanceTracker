using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace PersonalFinanceTracker.Filters
{
    /// <summary>
    /// Custom Authorization Filter
    /// Implements authorization logic using session-based authentication
    /// Respects [AllowAnonymous] attribute
    /// </summary>
    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if action or controller has [AllowAnonymous] attribute
            var allowAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                              || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();

            // Skip authentication check if [AllowAnonymous] is present
            if (allowAnonymous)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            // Check if user is logged in
            if (filterContext.HttpContext.Session["UserId"] == null)
            {
                // Redirect to login page
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" },
                        { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
