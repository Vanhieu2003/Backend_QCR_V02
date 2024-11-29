using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly HcmUeQTTB_DevContext _context;
        public TagRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }
        public async Task<List<TagsPerCriteria>> GetTagsPerCriteriaByTag(string tagId)
        {
            var tags = await _context.TagsPerCriteria.Where(t => t.TagId == tagId).ToListAsync();
            return tags;
        }

        //mới sửa
        public async Task<IEnumerable<TagGroupDto>> GetTagGroupsWithUserCountAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = from tag in _context.Tags
                        join userTag in _context.UserPerTags on tag.Id equals userTag.TagId into tagUsers
                        from userTag in tagUsers.DefaultIfEmpty()
                        group userTag by new { tag.Id, tag.TagName } into grouped
                        select new TagGroupDto
                        {
                            Id = grouped.Key.Id,
                            TagName = grouped.Key.TagName,
                            NumberOfUsers = grouped.Count(ut => ut != null)
                        };

            var tagDetails = await query
                .OrderByDescending(t => t.TagName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return tagDetails;
        }


        //mới sửa

        public async Task<List<ResponsibleTagDto>> GetGroupInfoByTagId(string tagId)
        {
            var result = await (from upt in _context.UserPerTags
                                join u in _context.Users on upt.UserId equals u.Id
                                join t in _context.Tags on upt.TagId equals t.Id
                                join uprg in _context.UserPerResGroups on u.Id equals uprg.UserId into userGroups
                                from uprg in userGroups.DefaultIfEmpty()
                                join rg in _context.ResponsibleGroups on uprg.ResponsiableGroupId equals rg.Id into groupNames
                                from rg in groupNames.DefaultIfEmpty()
                                where upt.TagId == tagId
                                group rg by new { u.Id, u.UserName, u.FirstName, u.LastName, u.Email, t.TagName } into grouped
                                select new ResponsibleTagDto
                                {
                                    Id = grouped.Key.Id,
                                    UserName = grouped.Key.UserName,
                                    FirstName = grouped.Key.FirstName,
                                    LastName = grouped.Key.LastName,
                                    Email = grouped.Key.Email,
                                    TagName = grouped.Key.TagName,
                                    GroupName = string.Join(", ", grouped.Select(g => g != null ? g.GroupName : "Không có nhóm phòng"))
                                })
                                .ToListAsync();

            return result;
        }

        public async Task<List<TagAverageRatingDto>> GetTagAverageRatingsAsync()
        {
            // Lấy danh sách các tag và các tiêu chí liên quan
            var tagRatings = await (from tag in _context.Tags
                                    join tagCriteria in _context.TagsPerCriteria on tag.Id equals tagCriteria.TagId
                                    join criteriaReport in _context.CriteriaReports on tagCriteria.CriteriaId equals criteriaReport.CriteriaId
                                    join criteria in _context.Criteria on tagCriteria.CriteriaId equals criteria.Id
                                    where criteriaReport.Value.HasValue // Chỉ lấy các giá trị không null
                                    group new { criteriaReport, criteria } by new { tag.TagName } into g
                                    select new
                                    {
                                        TagName = g.Key.TagName,
                                        TotalMaxValue = g.Sum(x => x.criteria.CriteriaType == "BINARY" ? 2 : 5),
                                        TotalValue = g.Sum(x => x.criteriaReport.Value ?? 0)
                                    }).ToListAsync();

            // Tính toán giá trị trung bình cho từng tag
            var tagAverageRatings = tagRatings.Select(tag => new TagAverageRatingDto
            {
                TagName = tag.TagName,
                AverageRating = tag.TotalMaxValue > 0 ? Math.Round((double)tag.TotalValue / tag.TotalMaxValue * 100) : 0
            }).ToList();

            return tagAverageRatings;
        }




        private DateTime GetCurrentTimeInGmtPlus7()
        {

            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus7);
        }
    }
}
