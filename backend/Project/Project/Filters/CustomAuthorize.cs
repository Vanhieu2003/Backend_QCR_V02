using backend.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Project.Service.User;

namespace backend.Filters
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

            if (!roleClaim.Value.Contains(RoleDefault.Administrator))
            {
                var service = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;// CH? check user verify v?i token g?i xu?ng hay ko 

                var _bearer_token = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (!await service.VerifyAsync(_bearer_token, _permissions))
                {
                    context.Result = new ForbidResult();
                }

                return;
            }
            return;
        }
    }
}
