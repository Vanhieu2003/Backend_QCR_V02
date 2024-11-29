using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsibleGroupsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IResponsibleGroupRepository _repo;


        public ResponsibleGroupsController(HcmUeQTTB_DevContext context, IResponsibleGroupRepository respoonsibleGroupRepository)
        {
            _context = context;
            _repo = respoonsibleGroupRepository;
        }

        // GET: api/ResponsibleGroups
        [HttpGet]
        public async Task<IActionResult> GetResponsibleGroups()
        {
            var result = await _repo.GetAllResponsiableGroup();
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var result = await _repo.GetAll(pageNumber, pageSize);
            return Ok(result);
        }



        // GET: api/ResponsibleGroups/5
        [HttpGet("id")]
        public async Task<ActionResult<ResponsiableGroupDto>> GetResponsibleGroup([FromQuery] string id)
        {
            var result = await _repo.GetAllResponsiableGroupById(id);

            if (result == null)
            {
                return NotFound();  
            }

            return Ok(result);  
        }


        [HttpPut]
        public async Task<IActionResult> UpdateResponsibleGroup([FromQuery] string id, [FromBody] ResponsibleGroupUpdateDto dto)
        {
            try
            {
                // Kiểm tra sự tồn tại của nhóm chịu trách nhiệm
                var group = await _context.ResponsibleGroups.FindAsync(id);
                if (group == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy nhóm chịu trách nhiệm với Id này." });
                }

                // Kiểm tra xem tên nhóm đã tồn tại (không phân biệt chữ hoa, chữ thường)
                var existingGroupByName = await _context.ResponsibleGroups
                    .FirstOrDefaultAsync(g => g.GroupName.ToLower() == dto.GroupName.ToLower() && g.Id != id);
                if (existingGroupByName != null)
                {
                    return BadRequest(new { success = false, message = "Tên nhóm đã tồn tại. Vui lòng chọn tên khác." });
                }

                // Kiểm tra xem màu đã tồn tại (không phân biệt chữ hoa, chữ thường)
                var existingGroupByColor = await _context.ResponsibleGroups
                    .FirstOrDefaultAsync(g => g.Color.ToLower() == dto.Color.ToLower() && g.Id != id);
                if (existingGroupByColor != null)
                {
                    return BadRequest(new { success = false, message = "Màu nhóm đã tồn tại. Vui lòng chọn màu khác." });
                }

                // Cập nhật các trường thông tin
                group.GroupName = dto.GroupName;
                group.Description = dto.Description;
                group.Color = dto.Color;

                // Xóa tất cả người dùng hiện tại khỏi nhóm
                var existingUsersInGroup = _context.UserPerResGroups.Where(upg => upg.ResponsiableGroupId == id);
                _context.UserPerResGroups.RemoveRange(existingUsersInGroup);

                // Thêm danh sách người dùng mới vào nhóm
                foreach (var userDto in dto.Users)
                {
                    var userPerResGroup = new UserPerResGroup
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userDto.Id,
                        ResponsiableGroupId = group.Id
                    };
                    _context.UserPerResGroups.Add(userPerResGroup);
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Cập nhật nhóm người chịu trách nhiệm thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateResponsiableGroupWithUser([FromBody] ResponsiableGroupDto dto)
        {
            try
            {
                // Kiểm tra xem tên nhóm đã tồn tại chưa
                var existingGroup = await _context.ResponsibleGroups
                    .FirstOrDefaultAsync(g => g.GroupName == dto.GroupName);

                if (existingGroup != null)
                {
                    // Nếu tên nhóm đã tồn tại, trả về lỗi
                    return BadRequest(new { success = false, message = "Tên nhóm đã tồn tại. Vui lòng chọn tên khác." });
                }

                // Tạo nhóm với Id tự động là Guid
                var group = new ResponsibleGroup
                {
                    Id = Guid.NewGuid().ToString(), // Tạo Guid cho RoomGroup
                    GroupName = dto.GroupName,
                    Description = dto.Description,
                    Color = dto.Color
                };

                _context.ResponsibleGroups.Add(group);
                await _context.SaveChangesAsync();

                // Thêm các phòng vào nhóm vừa tạo
                if (dto.Users != null)
                {
                    foreach (var userDto in dto.Users)
                    {
                        var userPerResGroup = new UserPerResGroup
                        {
                            Id = Guid.NewGuid().ToString(), // Tạo Guid cho RoomByGroup
                            UserId = userDto.Id,
                            ResponsiableGroupId = group.Id // Liên kết GroupRoomId với nhóm vừa tạo
                        };
                        _context.UserPerResGroups.Add(userPerResGroup);
                    }
                }
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Tạo nhóm người chịu trách nhiệm thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


    }
}
