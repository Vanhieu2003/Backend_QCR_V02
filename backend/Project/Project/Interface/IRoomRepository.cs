using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface IRoomRepository
    {
        public Task<List<RoomDto>> GetRoomByFloorIdAndBlockId(string floorId,string blockId);
        public Task<List<RoomDto>> GetRoomByFloorIdAndBlockIdIfFormExist(string floorId, string blockId);
        public Task<List<Room>> SearchRoom(string roomName);
        public Task<List<RoomDto>> GetRoomsByBlockAndCampusAsync(string blockId, string campusId);
        public Task<List<RoomDto>> GetRoomsByCampusAsync(string campusId);
    }
}
