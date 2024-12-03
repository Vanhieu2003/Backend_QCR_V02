using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface IShiftRepository
    {
        public Task<List<Shift>> GetShiftsByRoomId(string id);

    }
}
