using Project.dto;

namespace Project.Interface
{
    public interface IBlockRepository
    {
        public Task<IEnumerable<BlockDto>> GetBlocksByCampusIdAsync(string campusId);
    }
}
