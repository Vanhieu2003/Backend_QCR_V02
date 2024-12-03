using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class TagsPerCriteriaRepository : ITagsPerCriteriaRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public TagsPerCriteriaRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetTagsByCriteriaId(string id)
        {
            var tagsId = await _context.TagsPerCriteria
               .Where(x => x.CriteriaId == id)
               .Select(tpc => tpc.TagId)
               .ToListAsync();
            var tags = await _context.Tags
                .Where(t => tagsId.Contains(t.Id))
                .ToListAsync();
            return tags;
        }
    }
}
