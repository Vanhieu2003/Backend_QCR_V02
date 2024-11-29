using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;
using System.Drawing.Printing;

namespace Project.Repository
{
    public class GroupRoomRepository : IGroupRoomRepository
    {

        private readonly HcmUeQTTB_DevContext _context;

        public GroupRoomRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<GroupWithRoomsResponse> GetAllGroupWithRooms(int pageNumber = 1, int pageSize = 10)
        {

            var totalRecords = await (from rg in _context.RoomByGroups
                                      join grouproom in _context.GroupRooms on rg.GroupRoomId equals grouproom.Id
                                      select grouproom.Id)
                                     .Distinct()
                                     .CountAsync();

            var roomGroupDetails = await (from rg in _context.RoomByGroups
                                          join room in _context.Rooms on rg.RoomId equals room.Id
                                          join block in _context.Blocks on room.BlockId equals block.Id
                                          join grouproom in _context.GroupRooms on rg.GroupRoomId equals grouproom.Id
                                          group room by new
                                          {
                                              grouproom.Id,
                                              block.CampusName,
                                              grouproom.GroupName,
                                              grouproom.Description
                                          } into g
                                          select new GroupWithRoomsViewDto
                                          {
                                              Id = g.Key.Id,
                                              CampusName = g.Key.CampusName,
                                              GroupName = g.Key.GroupName,
                                              Description = g.Key.Description,
                                              NumberOfRoom = g.Count()
                                          })
                                         .OrderByDescending(s => s.CampusName)
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            return new GroupWithRoomsResponse
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                RoomGroups = roomGroupDetails
            };
        }

        public async Task<RoomGroupViewDto> GetRoomGroupById(string id)
        {

            var roomGroupData = await (from gr in _context.GroupRooms
                                       join rbg in _context.RoomByGroups on gr.Id equals rbg.GroupRoomId into rbgGroup
                                       from rbg in rbgGroup.DefaultIfEmpty()
                                       join r in _context.Rooms on rbg.RoomId equals r.Id into roomGroup
                                       from r in roomGroup.DefaultIfEmpty()
                                       join bl in _context.Blocks on r.BlockId equals bl.Id into blockGroup
                                       from bl in blockGroup.DefaultIfEmpty()
                                       join fl in _context.Floors on r.FloorId equals fl.Id into floorGroup
                                       from fl in floorGroup.DefaultIfEmpty()
                                       join rct in _context.RoomCategories on r.RoomCategoryId equals rct.Id into categoryGroup
                                       from rct in categoryGroup.DefaultIfEmpty()
                                       where gr.Id == id
                                       select new
                                       {
                                           GroupRoom = gr,
                                           Room = r,
                                           Block = bl,
                                           Floor = fl,
                                           Category = rct
                                       }).ToListAsync();

            if (roomGroupData == null || !roomGroupData.Any())
            {
                return null;
            }


            var roomGroupViewDto = new RoomGroupViewDto
            {
                Id = roomGroupData.First().GroupRoom.Id,
                GroupName = roomGroupData.First().GroupRoom.GroupName,
                Description = roomGroupData.First().GroupRoom.Description,
                Rooms = new List<RoomViewDto>()
            };


            foreach (var data in roomGroupData)
            {
                if (data.Room != null)
                {
                    var roomDto = new RoomViewDto
                    {
                        Id = data.Room.Id,
                        CampusName = data.Block?.CampusName,
                        CampusId = data.Block?.CampusId,
                        BlockName = data.Block?.BlockName,
                        FloorName = data.Floor?.FloorName,
                        RoomName = data.Room.RoomName,
                        CategoryName = data.Category?.CategoryName
                    };
                    roomGroupViewDto.Rooms.Add(roomDto);
                }
            }

            return roomGroupViewDto;
        }

    }




}
