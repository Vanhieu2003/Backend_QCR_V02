using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface ICriteriaRepository
    {
        public Task<List<Criteria>> GetCriteriasByRoomsCategoricalId(string id);

        public Task<List<Criteria>> GetAllCriteria();

        public Task<List<Criteria>> GetCriteriasByRoomId(string id);
        public Task<List<Criteria>> SearchCriteria(string keyword);
        public Task<Criteria> CreateCriteria(CreateCriteriaDto criteriaDto);
    }
}
