﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleRepository _repo;
        private readonly HcmUeQTTB_DevContext _context;

        public SchedulesController(HcmUeQTTB_DevContext context, IScheduleRepository repo)
        {
            _repo = repo;
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        [ClaimPermission(PermissionConstants.ViewSchedule)]
        public async Task<ActionResult<IEnumerable<ScheduleDetailInfoDto>>> GetSchedules()
        {
            var schedules = await _repo.GetSchedules();
            return Ok(schedules);
        }

        [HttpGet("userId")]
        [ClaimPermission(PermissionConstants.ViewSchedule)]
        public async Task<ActionResult<IEnumerable<ScheduleDetailInfoDto>>> GetSchedules([FromQuery] string userId)
        {
            var schedules = await _repo.GetSchedulesByUserId(userId);
            return Ok(schedules);
        }




        // GET: api/Schedules/GetRoomsList
        [HttpGet("GetRoomsList")]
        [ClaimPermission(PermissionConstants.ViewSchedule)]
        public async Task<IActionResult> GetRoomsListByRoomType([FromQuery] string RoomType)
        {
            var rooms = await _repo.GetListRoomByRoomType(RoomType);
            return Ok(rooms);
        }


        [HttpGet]
        [Route("get-users-by-shift-room-and-criteria")]
        [ClaimPermission(PermissionConstants.ViewSchedule)]
        public async Task<IActionResult> GetUsersByShiftRoomAndCriteria(

     [FromQuery] QRDto place,
     [FromQuery] List<string> criteriaIds)
        {
            // Bước 1: Lấy thời gian startTime và endTime của Shift dựa vào shiftId
            var shift = await _context.Shifts
                .Where(s => s.Id == place.ShiftId)
                .Select(s => new { s.StartTime, s.EndTime })
                .FirstOrDefaultAsync();

            // Bước 2: Lấy danh sách Schedule trong khoảng thời gian của shift
            var schedules = await _context.Schedules
            .Where(s => s.Start.TimeOfDay <= shift.StartTime &&
                        s.End.TimeOfDay >= shift.EndTime &&
                        s.Start.Date <= DateTime.Now.Date &&
                        s.End.Date >= DateTime.Now.Date)
            .Select(s => s.Id)
            .ToListAsync();

            // Bước 3: Tìm kiếm trong bảng ScheduleDetail các scheduleId và roomId trùng khớp
            var userIds = await GetUserByLevel(schedules, place);

            // Bước 4: Lấy danh sách TagId từ bảng TagsPerCriteria dựa trên danh sách criteriaIds
            var tagIdsFromCriteria = await _context.TagsPerCriteria
                .Where(tpc => criteriaIds.Contains(tpc.CriteriaId))
                .Select(tpc => tpc.TagId)
                .Distinct()
                .ToListAsync();

            // Bước 5: Lấy danh sách TagId và UserId từ bảng UserPerTag dựa vào userIds
            var userPerTags = await _context.UserPerTags
                .Where(upt => userIds.Contains(upt.UserId) && tagIdsFromCriteria.Contains(upt.TagId))
                .ToListAsync();

            // Bước 6: Lấy danh sách tất cả các Tag tương ứng với tagIds từ bảng Tags
            var tags = await _context.Tags
                .Where(t => tagIdsFromCriteria.Contains(t.Id))
                .ToListAsync();

            // Bước 7: Tạo một danh sách để trả về kết quả, gồm cả những Tag không có User nào
            var result = new List<object>();

            // Xử lý tất cả các tags lấy được từ criteria
            foreach (var tag in tags)
            {
                // Lấy tất cả các userId trong bảng UserPerTag tương ứng với tag.Id
                var usersInTag = userPerTags
                    .Where(upt => upt.TagId == tag.Id)
                    .Select(upt => upt.UserId)
                    .ToList();

                // Lấy thông tin các User dựa trên danh sách userIds trong tag này
                var users = await _context.Users
                    .Where(u => usersInTag.Contains(u.Id))
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Email = u.Email
                    })
                    .ToListAsync();

                // Thêm kết quả vào danh sách
                result.Add(new
                {
                    TagId = tag.Id,
                    TagName = tag.TagName,
                    Users = users // Trả về danh sách Users, nếu không có sẽ là danh sách rỗng
                });
            }

            // Bước 8: Đối với các TagId không có người dùng nào, thêm vào kết quả với danh sách Users rỗng
            foreach (var tagId in tagIdsFromCriteria.Except(tags.Select(t => t.Id)))
            {
                var tagName = await _context.Tags
                    .Where(t => t.Id == tagId)
                    .Select(t => t.TagName)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(tagName))
                {
                    result.Add(new
                    {
                        TagName = tagName,
                        Users = new List<object>() // Trả về danh sách Users rỗng nếu không có
                    });
                }
            }

            // Trả về kết quả
            return Ok(result);
        }



            [HttpPut]
        [ClaimPermission(PermissionConstants.ModifySchedule)]
        public async Task<IActionResult> UpdateSchedule([FromQuery] string id, [FromBody] ScheduleUpdateDto scheduleUpdateDto)
        {
            try
            {

                var existingSchedule = await _context.Schedules.FindAsync(id);
                if (existingSchedule == null)
                    return NotFound(new { success = false, message = "Lịch không tồn tại." });


                existingSchedule.Title = scheduleUpdateDto.Title;
                existingSchedule.Start = scheduleUpdateDto.StartDate;
                existingSchedule.End = scheduleUpdateDto.EndDate;
                existingSchedule.AllDay = scheduleUpdateDto.AllDay;
                existingSchedule.RecurrenceRule = scheduleUpdateDto.RecurrenceRule;
                existingSchedule.Description = scheduleUpdateDto.Description;
                existingSchedule.ResponsibleGroupId = scheduleUpdateDto.ResponsibleGroupId;


                if (scheduleUpdateDto.Users != null || scheduleUpdateDto.Place != null)
                {
                    var existingDetails = _context.ScheduleDetails.Where(sd => sd.ScheduleId == existingSchedule.Id);
                    _context.ScheduleDetails.RemoveRange(existingDetails);

                    foreach (var user in scheduleUpdateDto.Users)
                    {
                        foreach (var place in scheduleUpdateDto.Place)
                        {
                            foreach (var room in place.rooms)
                            {
                                var scheduleDetail = new ScheduleDetail
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    ScheduleId = existingSchedule.Id,
                                    UserId = user,
                                    RoomId = room.Id,
                                    RoomType = place.level,
                                };
                                _context.ScheduleDetails.Add(scheduleDetail);
                            }
                        }
                    }
                }


                await _context.SaveChangesAsync();


                return Ok(new
                {
                    success = true,
                    message = "Cập nhật lịch thành công.",
                    schedule = new
                    {
                        existingSchedule.Id,
                        existingSchedule.Title,
                        existingSchedule.Start,
                        existingSchedule.End,
                        existingSchedule.AllDay,
                        existingSchedule.RecurrenceRule,
                        existingSchedule.Description,
                        existingSchedule.ResponsibleGroupId
                    }
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { success = false, message = "Đã xảy ra lỗi khi cập nhật lịch.", error = ex.Message });
            }
        }




        [HttpPost]
        [ClaimPermission(PermissionConstants.ModifySchedule)]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleCreateDto scheduleCreateDto)
        {
            try
            {
               

                var indices = await _context.Schedules
                    .Select(s => s.Index)
                    .ToListAsync();

                var maxIndex = indices.DefaultIfEmpty(0).Max();

                var newSchedule = new Schedule
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = scheduleCreateDto.Title,
                    Start = scheduleCreateDto.StartDate,
                    End = scheduleCreateDto.EndDate,
                    AllDay = scheduleCreateDto.AllDay,
                    RecurrenceRule = scheduleCreateDto.RecurrenceRule,
                    Description = scheduleCreateDto.Description,
                    Index = maxIndex + 1,
                    ResponsibleGroupId = scheduleCreateDto.ResponsibleGroupId,
                };

                _context.Schedules.Add(newSchedule);

                if (scheduleCreateDto.Users != null && scheduleCreateDto.Place != null)
                {
                    foreach (var userId in scheduleCreateDto.Users)
                    {
                        foreach (var place in scheduleCreateDto.Place)
                        {
                            foreach (var room in place.rooms)
                            {
                                var scheduleDetail = new ScheduleDetail
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    ScheduleId = newSchedule.Id,
                                    UserId = userId,
                                    RoomId = room.Id,
                                    RoomType = place.level,
                                };
                                _context.ScheduleDetails.Add(scheduleDetail);
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Tạo lịch thành công.",
                    schedule = new
                    {
                        newSchedule.Id,
                        newSchedule.Title,
                        newSchedule.Start,
                        newSchedule.End,
                        newSchedule.AllDay,
                        newSchedule.RecurrenceRule,
                        newSchedule.Description,
                        newSchedule.ResponsibleGroupId,
                        newSchedule.Index // Trả về chỉ số của lịch mới
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Đã xảy ra lỗi khi tạo lịch.", error = ex.Message });
            }
        }



        [HttpDelete]
        [ClaimPermission(PermissionConstants.ModifySchedule)]
        public async Task<IActionResult> DeleteSchedule([FromQuery] string scheduleId)
        {
            try
            {

                if (string.IsNullOrEmpty(scheduleId))
                    return BadRequest(new { success = false, message = "Schedule ID không hợp lệ." });


                var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId);
                if (schedule == null)
                    return NotFound(new { success = false, message = "Lịch không tồn tại." });

                var scheduleDetails = await _context.ScheduleDetails.Where(sd => sd.ScheduleId == scheduleId).ToListAsync();
                if (scheduleDetails.Any())
                {
                    _context.ScheduleDetails.RemoveRange(scheduleDetails);
                }


                _context.Schedules.Remove(schedule);


                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Xóa lịch thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Đã xảy ra lỗi khi xóa lịch.", error = ex.Message });
            }
        }


        private async Task<List<string>> GetUserByLevel(List<string> schedule, QRDto place)
        {
            if (schedule == null)
            {
                return new List<string>();
            }

            var userIds = new List<string>();

            // Tìm UserId theo cấp CampusId
            userIds = await _context.ScheduleDetails
                .Where(sd => schedule.Contains(sd.ScheduleId) && sd.RoomId == place.CampusId)
                .Select(sd => sd.UserId)
                .Distinct()
                .ToListAsync();

            // Nếu có kết quả ở cấp CampusId, trả về
            if (userIds.Count > 0)
            {
                return userIds;
            }

            // Nếu không có kết quả ở cấp CampusId, tìm theo cấp BlockId
            userIds = await _context.ScheduleDetails
                .Where(sd => schedule.Contains(sd.ScheduleId) && sd.RoomId == place.BlockId)
                .Select(sd => sd.UserId)
                .Distinct()
                .ToListAsync();

            // Nếu có kết quả ở cấp BlockId, trả về
            if (userIds.Count > 0)
            {
                return userIds;
            }

            // Nếu không có kết quả ở cấp BlockId, tìm theo cấp FloorId
            userIds = await _context.ScheduleDetails
                .Where(sd => schedule.Contains(sd.ScheduleId) && sd.RoomId == place.FloorId)
                .Select(sd => sd.UserId)
                .Distinct()
                .ToListAsync();

            // Nếu có kết quả ở cấp FloorId, trả về
            if (userIds.Count > 0)
            {
                return userIds;
            }

            // Nếu không có kết quả ở cấp FloorId, tìm theo cấp RoomId
            userIds = await _context.ScheduleDetails
                .Where(sd => schedule.Contains(sd.ScheduleId) && sd.RoomId == place.RoomId)
                .Select(sd => sd.UserId)
                .Distinct()
                .ToListAsync();

            return userIds;
        }
    }
}
