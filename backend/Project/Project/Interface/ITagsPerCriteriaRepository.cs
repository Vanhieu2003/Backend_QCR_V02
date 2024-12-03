using Project.Entities;

namespace Project.Interface
{
    public interface ITagsPerCriteriaRepository
    {
        public Task<List<Tag>> GetTagsByCriteriaId(string criteriaId);
    }
}
