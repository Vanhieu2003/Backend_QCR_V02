using Project.Dto;
using Project.Entities;

namespace Project.Interface
{
    public interface ICleaningReportRepository
    {
        public Task<List<CleaningReport>> GetReportsByShiftId(string shiftId);
        public Task<List<CleaningReport>> GetCleaningReportByCleaningForm(string formId);

        public Task<CleaningReportDetailsDto> GetInfoByReportId(string reportId);

        public Task<List<CleaningReportDetailsDto>> GetReportInfo();
        public Task<CleaningReport> CreateCleaningReportAsync(CleaningReportRequest request);
        public Task<object> GetReportDetails(string reportId);

        public  Task<List<UserScore>> EvaluateUserScores(EvaluationRequest request);

        public Task<CleaningReport> UpdateCriteriaAndCleaningReport(UpdateCleaningReportRequest request);

        public  Task<List<CleaningReportDetailsDto>> GetReportInfoByUserId(string userId);
        public  Task<List<CleaningReportDetailsDto>> GetReportInfoByManageUserId(string userId);

    }
}
