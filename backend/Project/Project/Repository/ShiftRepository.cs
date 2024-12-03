using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public ShiftRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<bool> IsDuplicateShiftNameAsync(string shiftName, List<string> categories)
        {
            return await _context.Shifts
                .Where(s => categories.Contains(s.RoomCategoryId))
                .AnyAsync(s => s.ShiftName == shiftName);
        }

        public async Task<bool> IsOverlappingShiftAsync(TimeSpan startTime, TimeSpan endTime, List<string> categories)
        {
            return await _context.Shifts
                .Where(s => categories.Contains(s.RoomCategoryId))
                .AnyAsync(s => startTime < s.EndTime && endTime > s.StartTime);
        }

        public async Task<List<Shift>> CreateShiftsAsync(string shiftName, TimeSpan startTime, TimeSpan endTime, List<string> categories)
        {
            var shiftsToCreate = categories.Select(roomCategoryId => new Shift
            {
                Id = Guid.NewGuid().ToString(),
                ShiftName = shiftName,
                StartTime = startTime,
                EndTime = endTime,
                RoomCategoryId = roomCategoryId,
                Status = "ENABLE",
                CreateAt = GetCurrentTimeInGmtPlus7(),
                UpdateAt = GetCurrentTimeInGmtPlus7()
            }).ToList();

            _context.Shifts.AddRange(shiftsToCreate);
            await _context.SaveChangesAsync();

            return shiftsToCreate;
        }







        public async Task<List<Shift>> GetShiftsByRoomId(string id)
        {
            var room = await _context.Rooms.Where(r=>r.Id == id).FirstOrDefaultAsync();
       
            var shifts = await _context.Shifts
                .Where(x => x.RoomCategoryId == room.RoomCategoryId && x.Status == "ENABLE")
                .OrderBy(x => x.ShiftName)
                .ToListAsync();

            return shifts;
        }


        private DateTime GetCurrentTimeInGmtPlus7()
        {

            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus7);
        }
    }
}
