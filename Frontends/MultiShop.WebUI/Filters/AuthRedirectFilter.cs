using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MultiShop.WebUI.Filters
{
    public class AuthRedirectFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
           .Any(em => em is AllowAnonymousAttribute);

            if (allowAnonymous)
                return Task.CompletedTask;
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new RedirectToActionResult(
                    "Index", "Login", null);
            }

            return Task.CompletedTask;
        }
    }
}
