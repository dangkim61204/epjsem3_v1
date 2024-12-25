using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace StarSecurityServices.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? user = User.Identity?.Name;
            string? role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Admin" || role != "Manager" || role != "Staff")
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "AccessDenied" }
                });
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
