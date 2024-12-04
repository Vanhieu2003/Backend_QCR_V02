using System;
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
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IShiftRepository _repo;

        public ShiftsController(HcmUeQTTB_DevContext context, IShiftRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult> GetShifts()
            
        {
            var shifts = from s in _context.Shifts
                         join roomCategory in _context.RoomCategories
                         on s.RoomCategoryId equals roomCategory.Id
                         select new ShiftViewDto
                         {
                             Id = s.Id,
                             ShiftName = s.ShiftName,
                             StartTime = s.StartTime,
                             EndTime = s.EndTime,
                             CategoryName = roomCategory.CategoryName,
                             roomCategoryId = roomCategory.Id,
                             Status = s.Status,
                         };

           

            return Ok (await shifts.ToListAsync());
        }

        // API lấy thông tin ca làm việc theo id, dùng query parameter
        [HttpGet("id")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<Shift>> GetShiftById([FromQuery] string id)
        {
            if (_context.Shifts == null)
            {
                return NotFound();
            }
            var shift = await _context.Shifts.FindAsync(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        [HttpGet("ByRoomId")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetShiftsByRoomId([FromQuery] string roomId)
        {
            var shifts = await _repo.GetShiftsByRoomId(roomId);
            if (shifts == null)
            {
                return NotFound();
            }
            return Ok(shifts);
        }

        [HttpPut]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<IActionResult> PutShift([FromQuery] string id, [FromBody] ShiftUpdateDto shiftDto)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy ca làm việc." });
            }

            TimeSpan startTime;
            TimeSpan endTime;

            if (!TimeSpan.TryParse(shiftDto.StartTime, out startTime))
            {
                return BadRequest(new { success = false, message = "StartTime không hợp lệ." });
            }

            if (!TimeSpan.TryParse(shiftDto.EndTime, out endTime))
            {
                return BadRequest(new { success = false, message = "EndTime không hợp lệ." });
            }

            var duplicateShiftName = await _context.Shifts
                .Where(s => s.RoomCategoryId == shift.RoomCategoryId && s.ShiftName == shiftDto.ShiftName && s.Id != id)
                .FirstOrDefaultAsync();

            if (duplicateShiftName != null)
            {
                return Conflict(new { success = false, message = "Tên ca đã tồn tại trong khu vực này." });
            }

            var overlappingShift = await _context.Shifts
                .Where(s => s.RoomCategoryId == shift.RoomCategoryId && s.Id != id)
                .Where(s => startTime < s.EndTime && endTime > s.StartTime)
                .FirstOrDefaultAsync();

            if (overlappingShift != null)
            {
                return Conflict(new { success = false, message = "Ca làm việc đã tồn tại trong khoảng thời gian này cho khu vực này." });
            }

            shift.ShiftName = shiftDto.ShiftName;
            shift.StartTime = startTime;
            shift.EndTime = endTime;
            shift.UpdateAt = DateTime.Now;

            shift.Status = !string.IsNullOrEmpty(shiftDto.Status) ? shiftDto.Status : "ENABLE";

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
                {
                    return NotFound(new { success = false, message = "Không tìm thấy ca làm việc." });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { success = true, message = "Cập nhật ca làm thành công.", shift });
        }

        [HttpPost]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<ActionResult<Shift>> PostShift([FromBody] ShiftCreateDto shiftDto)
        {
            TimeSpan startTime;
            TimeSpan endTime;

            if (!TimeSpan.TryParse(shiftDto.StartTime, out startTime))
            {
                return BadRequest(new { success = false, message = "StartTime không hợp lệ." });
            }

            if (!TimeSpan.TryParse(shiftDto.EndTime, out endTime))
            {
                return BadRequest(new { success = false, message = "EndTime không hợp lệ." });
            }

            if (shiftDto.Category == null || !shiftDto.Category.Any())
            {
                return BadRequest(new { success = false, message = "Category không hợp lệ." });
            }

            var duplicateShiftName = await _context.Shifts
                .Where(s => shiftDto.Category.Contains(s.RoomCategoryId))
                .Where(s => s.ShiftName == shiftDto.ShiftName)
                .FirstOrDefaultAsync();

            if (duplicateShiftName != null)
            {
                return Conflict(new { success = false, message = "Tên ca đã tồn tại trong khu vực này." });
            }

            var overlappingShift = await _context.Shifts
                .Where(s => shiftDto.Category.Contains(s.RoomCategoryId))
                .Where(s => startTime < s.EndTime && endTime > s.StartTime)
                .FirstOrDefaultAsync();

            if (overlappingShift != null)
            {
                return Conflict(new { success = false, message = "Ca làm việc đã tồn tại trong khoảng thời gian này cho khu vực này." });
            }

            var shiftsToCreate = new List<Shift>();

            foreach (var roomCategoryId in shiftDto.Category)
            {
                var shift = new Shift
                {
                    Id = Guid.NewGuid().ToString(),
                    ShiftName = shiftDto.ShiftName,
                    StartTime = startTime,
                    EndTime = endTime,
                    RoomCategoryId = roomCategoryId,
                    Status = "ENABLE",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                shiftsToCreate.Add(shift);
            }

            _context.Shifts.AddRange(shiftsToCreate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new { success = false, message = "Có lỗi khi tạo ca." });
            }

            return CreatedAtAction("GetShiftById", new { id = shiftsToCreate.First().Id }, new { success = true, message = "Tạo ca làm thành công.", shifts = shiftsToCreate });
        }

       

        private bool ShiftExists(string id)
        {
            return (_context.Shifts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
