using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface ICleaningReportRepository
    {
        public Task<List<CleaningReport>> GetReportsByShiftId(string shiftId);
        public Task<List<CleaningReport>> GetCleaningReportByCleaningForm(string formId);

        public Task<CleaningReportDetailsDto> GetInfoByReportId(string reportId);

        public Task<(List<CleaningReportDetailsDto> Reports, int TotalCount)> GetReportInfo(int pageNumber = 1, int pageSize = 10);
        public Task<CleaningReport> CreateCleaningReportAsync(CleaningReportRequest request);
        public Task<object> GetReportDetails(string reportId);

        public  Task<List<UserScore>> EvaluateUserScores(EvaluationRequest request);

        public Task<CleaningReport> UpdateCriteriaAndCleaningReport(UpdateCleaningReportRequest request);

    }
}
