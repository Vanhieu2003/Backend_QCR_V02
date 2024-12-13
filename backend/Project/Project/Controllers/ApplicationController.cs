using backend.Constants;
using backend.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Interface;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _applicationService;

        public ApplicationController(IApplicationRepository applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [ClaimPermission(PermissionConstants.AccessQcr)]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await _applicationService.GetApplicationsAsync();
            return Ok(applications);
        }
    }
}
