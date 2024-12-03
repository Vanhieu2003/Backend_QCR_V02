using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class CleaningFormRepository : ICleaningFormRepository
    {
        private readonly HcmUeQTTB_DevContext _context;
        public CleaningFormRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }
        public async Task<CleaningForm> GetCleaningFormByRoomId(string roomId)
        {
            return await _context.CleaningForms.Where(x => x.RoomId == roomId).FirstOrDefaultAsync();
        }

        public async Task<List<CleaningForm>> GetAllCleaningForm(int pageNumber = 1, int pageSize = 10)
        {
            var query = from cleaningForm in _context.CleaningForms
                        join room in _context.Rooms on cleaningForm.RoomId equals room.Id
                        join block in _context.Blocks on room.BlockId equals block.Id
                        join campus in _context.Campuses on block.CampusId equals campus.Id
                        select new
                        {
                            CleaningForm = cleaningForm,
                            SortOrder = campus.SortOrder
                        };

            query = query.OrderBy(cf => cf.SortOrder);

            var cleaningFormsDetails = await query.Skip((pageNumber - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .Select(cf => cf.CleaningForm)
                                                  .ToListAsync();

            return cleaningFormsDetails;
        }

        public async Task<List<Criteria>> GetCriteriaByFormId(string formId)
        {
            var roomId = await _context.CleaningForms.Where(x => x.Id == formId).Select(x => x.RoomId).FirstOrDefaultAsync();
            var roomCategoryId = await _context.Rooms.Where(x => x.Id == roomId).Select(x => x.RoomCategoryId).FirstOrDefaultAsync();
            var criteriaList = await _context.Criteria.Where(x => x.RoomCategoryId == roomCategoryId).ToListAsync();
            return criteriaList;
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByFloorIdWithFormAsync(string floorId)
        {
            var rooms = await (from room in _context.Rooms
                               join cleaningForm in _context.CleaningForms on room.Id equals cleaningForm.RoomId
                               where room.FloorId == floorId
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