using Project.Dto;

namespace Project.Interface
{
    public interface IGroupRoomRepository
    {
        public Task<GroupWithRoomsResponse> GetAllGroupWithRooms(int pageNumber = 1, int pageSize = 10);
        public Task<RoomGroupViewDto> GetRoomGroupById(string id);
    }
}
