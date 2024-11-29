using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public RoomRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }
        public async Task<List<RoomDto>> GetRoomByFloorIdAndBlockId(string floorId,string blockId)
        {
            var rooms = await _context.Rooms
        .Where(x => x.FloorId == floorId && x.BlockId == blockId)
        .OrderBy(x => x.SortOrder) 
        .ToListAsync();

            
            var roomDtos = rooms.Select(room => new RoomDto
            {
                Id = room.Id,
                RoomName = room.RoomName,
                FloorId = room.FloorId,
                BlockId = room.BlockId,
                RoomCategoryId = room.RoomCategoryId
               
            }).ToList();

            return roomDtos;
        }

        public async Task<List<RoomDto>> GetRoomByFloorIdAndBlockIdIfFormExist(string floorId,string blockId)
        {
            var rooms = await _context.Rooms
        .Where(room => room.FloorId == floorId && room.BlockId == blockId)
        .Join(_context.CleaningForms,
              room => room.Id,
              form => form.RoomId,
              (room, form) => room)
        .GroupBy(room => room.Id)
        .Select(group => group.First())
        .ToListAsync();

            
            var roomDtos = rooms.Select(room => new RoomDto
            {
                Id = room.Id,
                RoomName = room.RoomName,
                FloorId = room.FloorId,
                BlockId = room.BlockId,
                RoomCategoryId = room.RoomCategoryId
               
            }).ToList();

            return roomDtos;
        }

        public async Task<List<Room>> SearchRoom(string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                return await _context.Rooms.Take(50).ToListAsync();
            }

            return await _context.Rooms
                .Where(r => r.RoomName.Contains(roomName))
                .ToListAsync();
        }


        public async Task<List<RoomDto>> GetRoomsByBlockAndCampusAsync(string blockId, string campusId)
        {
            var rooms = await (from room in _context.Rooms
                               join block in _context.Blocks
                               on room.BlockId equals block.Id
                               orderby room.SortOrder
                               where room.BlockId == blockId && block.CampusId == campusId
                     
                               select new RoomDto
                               {
                                   Id = room.Id,
                                   RoomName = room.RoomName,
                                   FloorId = room.FloorId,
                                   BlockId = room.BlockId,
                                   RoomCategoryId = room.RoomCategoryId
                               }).ToListAsync();

            return rooms;
        }

        public async Task<List<RoomDto>> GetRoomsByCampusAsync(string campusId)
        {
            var rooms = await (from room in _context.Rooms
                               join block in _context.Blocks
                               on room.BlockId equals block.Id
                               where block.CampusId == campusId
                               orderby room.SortOrder
                               select new RoomDto
                               {
                                   Id = room.Id,
                                   RoomName = room.RoomName,
                                   FloorId = room.FloorId,
                                   BlockId = room.BlockId,
                                   RoomCategoryId = room.RoomCategoryId
                               }).ToListAsync();

            return rooms;
        }
    }
}
