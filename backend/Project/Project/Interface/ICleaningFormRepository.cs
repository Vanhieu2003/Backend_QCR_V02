using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface ICleaningFormRepository
    {
        public Task<List<Criteria>> GetCriteriaByFormId(string formId);
        public Task<CleaningForm> GetCleaningFormByRoomId(string roomId);

        public Task<List<CleaningForm>> GetAllCleaningForm(int pageNumber = 1, int pgaeSize = 10);

        public Task<IEnumerable<RoomDto>> GetRoomsByFloorIdWithFormAsync(string floorId);

    }
}
