using Project.Dto;

namespace Project.Interface
{
    public interface IFloorRepository
    {
        public Task<List<FloorDto>> GetFloorByBlockId(string id);
    }
}
