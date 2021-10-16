using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DowntimeAlerter.WebApi.ActionFilters
{
    public class LoginFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
            var userid = context.HttpContext.Request.Cookies["id"];
            if (userid == null) context.Result = new RedirectResult("/Login/Login");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}