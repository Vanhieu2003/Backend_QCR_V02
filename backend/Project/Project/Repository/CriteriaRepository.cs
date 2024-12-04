using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class CriteriaRepository : ICriteriaRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public CriteriaRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<Criteria> CreateCriteria(CreateCriteriaDto criteriaDto)
        {

            var existingCriteria = await _context.Criteria
        .FirstOrDefaultAsync(c => c.CriteriaName == criteriaDto.CriteriaName
                                   && c.RoomCategoryId == criteriaDto.RoomCategoryId
                                   && c.Status == "ENABLE");

            if (existingCriteria != null)
            {
                throw new Exception($"CriteriaName '{criteriaDto.CriteriaName}' đã tồn tại trong RoomCategory '{criteriaDto.RoomCategoryId}'.");
            }


            var newCriteria = new Criteria
            {
                Id = Guid.NewGuid().ToString(),
                CriteriaName = criteriaDto.CriteriaName,
                RoomCategoryId = criteriaDto.RoomCategoryId,
                CriteriaType = criteriaDto.CriteriaType,
                CreateAt = GetCurrentTimeInGmtPlus7(),
                UpdateAt = GetCurrentTimeInGmtPlus7(),
                Status = "ENABLE"
            };

            _context.Criteria.Add(newCriteria);
            await _context.SaveChangesAsync();

      
            foreach (var tagName in criteriaDto.Tags)
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
                if (tag == null)
                {
                    
                    tag = new Tag
                    {
                        Id = Guid.NewGuid().ToString(),
                        TagName = tagName,
                        CreateAt = GetCurrentTimeInGmtPlus7(),
                        UpdateAt = GetCurrentTimeInGmtPlus7()
                    };
                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync();
                }

                
                var tagsPerCriteria = new TagsPerCriteria
                {
                    Id = Guid.NewGuid().ToString(),
                    TagId = tag.Id,
                    CriteriaId = newCriteria.Id,
                    CreateAt = GetCurrentTimeInGmtPlus7(),
                    UpdateAt = GetCurrentTimeInGmtPlus7()
                };
                _context.TagsPerCriteria.Add(tagsPerCriteria);
            }

            await _context.SaveChangesAsync();

            return newCriteria;
        }

        public async Task<List<Criteria>> GetAllCriteria()
        {
            var query = _context.Criteria.Where(c => c.Status == "ENABLE").AsQueryable();

            query = query.OrderByDescending(r => r.RoomCategoryId);

            var criteriaDetail = await query.ToListAsync();

            return criteriaDetail;
        }

        public async Task<List<Criteria>> GetCriteriasByRoomId(string id)
        {
            var roomcategoryId = await _context.Rooms.Where(x => x.Id == id).Select(x => x.RoomCategoryId).FirstOrDefaultAsync();
            var criteriaList = await _context.Criteria.Where(x => x.RoomCategoryId == roomcategoryId && x.Status== "ENABLE").ToListAsync();
            return criteriaList;
        }

        public async Task<List<Criteria>> GetCriteriasByRoomsCategoricalId(string id)
        {
            var criteriaList = await _context.Criteria.Where(x => x.RoomCategoryId == id).ToListAsync();
            return criteriaList;
        }
        public async Task<List<Criteria>> SearchCriteria(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _context.Criteria.Where(x => x.Status == "ENABLE").ToListAsync();
            }
            return await _context.Criteria
                .Where(c => c.CriteriaName.Contains(keyword) && c.Status == "ENABLE") 
                .ToListAsync();
        }


        private DateTime GetCurrentTimeInGmtPlus7()
        {

            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus7);
        }
    }
}
