using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface IScheduleRepository
    {
        public Task<Schedule> CreateSchedule (ScheduleDto schedule);
        public Task<List<RoomItemDto>> GetListRoomByRoomType (string roomType);
        Task<List<UserDto>> GetResponsibleUsersForRoomAndShift(string roomId, string shiftId);  
    }
}
