using Project.Entities;

namespace Project.Interface
{
    public interface ICriteriaReportRepository
    {
        public Task<List<CriteriaReport>> GetReportByCriteriaId(string criteriaId);
        public  Task<CriteriaReport> GetExistingReportAsync(CriteriaReport criteriaReport);
        public Task AddCriteriaReportAsync(CriteriaReport criteriaReport);
    }
}
