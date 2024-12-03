using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class CriteriaReportRepository : ICriteriaReportRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public CriteriaReportRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<List<CriteriaReport>> GetReportByCriteriaId(string criteriaId)
        {
            return await _context.CriteriaReports
                .Where(cr => cr.CriteriaId == criteriaId)
                .ToListAsync();
        }

        public async Task<CriteriaReport> GetExistingReportAsync(CriteriaReport criteriaReport)
        {
            return await _context.CriteriaReports
                .FirstOrDefaultAsync(cr => cr.CriteriaId == criteriaReport.CriteriaId
                                        && cr.ReportId == criteriaReport.ReportId
                                        && cr.FormId == criteriaReport.FormId);
        }

        // Thêm mới CriteriaReport
        public async Task AddCriteriaReportAsync(CriteriaReport criteriaReport)
        {
            criteriaReport.CreateAt = GetCurrentTimeInGmtPlus7();
            criteriaReport.UpdateAt = GetCurrentTimeInGmtPlus7();
            _context.CriteriaReports.Add(criteriaReport);
            await _context.SaveChangesAsync();
        }

        private DateTime GetCurrentTimeInGmtPlus7()
        {

            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus7);
        }
    }
}
