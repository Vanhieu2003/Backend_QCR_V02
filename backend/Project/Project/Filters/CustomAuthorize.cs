using backend.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Project.Service.User;
using Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace Project.Filters
{
    public class ClaimPermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        readonly string[] _permissions;
        public ClaimPermissionAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Role);
            var userId = context.HttpContext.User.Claims.FirstOrDefault(o => o.Type == ExClaimNames.UserId);

            if (_permissions == null || _permissions.Length == 0)
            {
                return;
            }

            if (!roleClaim.Value.Contains("A239AAC5-48FE-4446-BC0E-239AB1E659DD"))
            {
                var service = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
                var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<ClaimPermissionAttribute>)) as ILogger<ClaimPermissionAttribute>;

                if (!await service.VerifyAccess(userId.Value, _permissions))
                {
                    context.Result = new ForbidResult();
                }

                return;
            }
            return;
        }
    }
}


