using Project.Dto;

namespace Project.Interface
{
    public interface IGroupRoomRepository
    {
        public Task<List<GroupWithRoomsViewDto>> GetAllGroupWithRooms();
        public Task<RoomGroupViewDto> GetRoomGroupById(string id);
    }
}
