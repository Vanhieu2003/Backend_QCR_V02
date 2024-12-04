using backend.Constants;
using backend.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Dto;
using Project.Entities;
using Project.Interface;
using Project.Repository;
using System.Data.Entity;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersPerTagController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IUserPerTagRepository _repo;

        public UsersPerTagController(HcmUeQTTB_DevContext context, IUserPerTagRepository repo)
        {

            _context = context;
            _repo = repo;
        }

        [HttpPost]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<IActionResult> CreateUsersPerTag([FromBody] AssignUserRequest request)
        {
            try
            {
                await _repo.CreateUserPerGroup(request);
                return Ok("Thêm thành công");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<IActionResult> UpdateUsersPerTag([FromBody] UpdateUserRequest request)
        {
            try
            {
                await _repo.UpdateUsersForTag(request);
                return Ok("Chỉnh sửa thành công");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
