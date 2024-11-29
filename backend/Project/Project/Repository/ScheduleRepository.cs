using Microsoft.AspNetCore.Mvc;
using Project.Dto;
using Project.Entities;
using Project.Interface;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public ScheduleRepository(HcmUeQTTB_DevContext context) {
            _context = context;
        }

        public async Task<Schedule>  CreateSchedule (ScheduleDto scheduleDto)
        {
            if (scheduleDto == null)
                return null;

            var newSchedule = new Schedule
            {
                Id = Guid.NewGuid().ToString(),
                Title = scheduleDto.Subject,
                Start = scheduleDto.StartTime,
                End = scheduleDto.EndTime,
                AllDay = scheduleDto.IsAllDay,
                RecurrenceRule = scheduleDto.RecurrenceRule,
                Description = scheduleDto.Description,
                Index = scheduleDto.Id,
                ResponsibleGroupId = scheduleDto.ResponsibleGroupId,

            };

            _context.Schedules.Add(newSchedule);
            foreach (var userId in scheduleDto.Users)
            {
                // Thêm vào ScheduleDetail
                foreach (var place in scheduleDto.Place)
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

            await _context.SaveChangesAsync();
            return newSchedule;
        }

        public async Task<List<RoomItemDto>> GetListRoomByRoomType(string roomType)
        {
            var room = new List<RoomItemDto>();

            switch (roomType)
            {
                case "Cơ sở":
                    room =  _context.Campuses
                        .Select(c => new RoomItemDto{ Id = c.Id, Name = c.CampusName })
                        .ToList();
                    break;
                case "Tòa nhà":
                    room =  _context.Blocks
                        .Select(b => new RoomItemDto { Id = b.Id, Name = b.BlockName })
                        .ToList();
                    break;
                case "Tầng":
                    room = (from f in _context.Floors
                            join fb in _context.FloorOfBlocks on f.Id equals fb.FloorId
                            join b in _context.Blocks on fb.BlockId equals b.Id
                            select new RoomItemDto
                            {
                                Id = f.Id,
                                Name = $"{b.BlockCode} - {f.FloorName}",
                            }).ToList();
                    break;
                case "Phòng":
                    room = _context.Rooms
                        .Select(r => new RoomItemDto { Id = r.Id, Name = r.RoomName })
                        .ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid room type");
            }

            return room;
        }
        public async Task<List<UserDto>> GetResponsibleUsersForRoomAndShift(string roomId, string shiftId)
        {
            
            var responsibleUsers = await _context.ScheduleDetails
                .Where(sd => sd.RoomId == roomId && sd.ScheduleId == shiftId)
                .Join(_context.Users,
                    sd => sd.UserId,
                    u => u.Id,
                    (sd, u) => new UserDto
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Email = u.Email
                    })
                .Distinct()
                .ToListAsync();

            return responsibleUsers;
        }
    }
}
