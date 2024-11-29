using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class CriteriasPerFormRepository : ICriteriasPerFormRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public CriteriasPerFormRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<List<Criteria>> GetCriteriaByFormId(string formId)
        {
            var criteriaIds = await _context.CriteriasPerForms
                                    .Where(cp => cp.FormId == formId)
                                    .Select(cp => cp.CriteriaId)
                                    .ToListAsync();

            // Lấy chi tiết các criteria dựa trên danh sách criteriaId
            var criteriaList = await _context.Criteria
                                             .Where(c => criteriaIds.Contains(c.Id))
                                             .ToListAsync();

            // Trả về danh sách criteria
            return criteriaList;
        }
    }
}
