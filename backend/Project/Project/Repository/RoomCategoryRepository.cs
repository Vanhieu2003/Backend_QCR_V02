using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class RoomCategoryRepository : IRoomCategoryRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public RoomCategoryRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<List<RoomCategory>> GetRoomCategoriesbyCriteriaId(string criteriaId)
        {
            return await (from criteria in _context.Criteria
                          join roomCategory in _context.RoomCategories
                          on criteria.RoomCategoryId equals roomCategory.Id
                          where criteria.Id == criteriaId
                          select roomCategory).ToListAsync();
        }


    }
}