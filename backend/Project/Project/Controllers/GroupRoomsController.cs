﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Constants;
using backend.Filters;
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
    public class GroupRoomsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IGroupRoomRepository _roomRepository;

        public GroupRoomsController(HcmUeQTTB_DevContext context, IGroupRoomRepository groupRoomRepository)
        {
            _context = context;
            _roomRepository = groupRoomRepository;
        }

        [HttpGet]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetGroupRooms()
        {
            var result = await _roomRepository.GetAllGroupWithRooms();
            return Ok(result);
        }


        [HttpGet("id")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<RoomGroupViewDto>> GetRoomGroupById([FromQuery] string id)
        {
            var result = await _roomRepository.GetRoomGroupById(id);
            return Ok(result); 
        }

        [HttpPut]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<IActionResult> UpdateRoomGroup([FromQuery] string id, [FromBody] RoomGroupUpdateDto dto)
        {
            try
            {
              
                var group = await _context.GroupRooms.FindAsync(id);
                if (group == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy nhóm phòng với Id này." });
                }

               
                var existingGroupByName = await _context.GroupRooms
                    .FirstOrDefaultAsync(g => g.GroupName.ToLower() == dto.GroupName.ToLower() && g.Id != id);
                if (existingGroupByName != null)
                {
                    return BadRequest(new { success = false, message = "Tên nhóm đã tồn tại. Vui lòng chọn tên khác." });
                }



               
                group.GroupName = dto.GroupName;
                group.Description = dto.Description;


              
                var existingRoomsInGroup = _context.RoomByGroups.Where(rbg => rbg.GroupRoomId == id);
                _context.RoomByGroups.RemoveRange(existingRoomsInGroup);

               
                foreach (var roomDto in dto.Rooms)
                {
                    var roomByGroup = new RoomByGroup
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoomId = roomDto.Id,
                        GroupRoomId = group.Id
                    };
                    _context.RoomByGroups.Add(roomByGroup);
                }

                
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Cập nhật nhóm phòng thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<IActionResult> CreateGroupWithRooms([FromBody] GroupWithRoomsDto dto)
        {
            try
            {
               
                var existingGroup = await _context.GroupRooms
                    .FirstOrDefaultAsync(g => g.GroupName == dto.GroupName);

                if (existingGroup != null)
                {
                    
                    return BadRequest(new { success = false, message = "Tên nhóm đã tồn tại. Vui lòng chọn tên khác." });
                }

               
                var group = new GroupRoom
                {
                    Id = Guid.NewGuid().ToString(), 
                    GroupName = dto.GroupName,
                    Description = dto.Description
                };

                _context.GroupRooms.Add(group);
                await _context.SaveChangesAsync();

               
                foreach (var roomDto in dto.Rooms)
                {
                    var roomByGroup = new RoomByGroup
                    {
                        Id = Guid.NewGuid().ToString(), 
                        RoomId = roomDto.Id,
                        GroupRoomId = group.Id 
                    };
                    _context.RoomByGroups.Add(roomByGroup);
                }

                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Nhóm phòng được tạo thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }



        
    }
}
