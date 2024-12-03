using Project.Entities;

namespace Project.Interface
{
    public interface IRoomCategoryRepository
    {
        public Task<List<RoomCategory>> GetRoomCategoriesbyCriteriaId(string criteriaId);
    }
}
