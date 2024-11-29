using Project.Entities;

namespace Project.Interface
{
    public interface ICriteriasPerFormRepository
    {
        public Task<List<Criteria>> GetCriteriaByFormId(string formId);
    }
}
