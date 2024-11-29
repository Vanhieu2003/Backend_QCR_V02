using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface IResponsibleGroupRepository
    {
        public Task<List<ResponsibleGroup>> GetAllResponsiableGroup();
        public Task<ResponsiableGroupResponse> GetAll(int pageNumber = 1, int pageSize = 10);

        public Task<ResponsiableGroupDto> GetAllResponsiableGroupById(string Id);
    }
}
