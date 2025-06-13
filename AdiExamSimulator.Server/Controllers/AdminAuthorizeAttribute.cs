using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AdiExamSimulator.Server.Controllers
{
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var isAdmin = user.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
            if (isAdmin != "True")
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
