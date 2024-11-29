using Microsoft.AspNetCore.Mvc;
using Project.Dto;
using Project.Entities;
using Project.Interface;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public class UserPerTagRepository : IUserPerTagRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public UserPerTagRepository(HcmUeQTTB_DevContext context) {
            _context = context;
        }
        public async Task CreateUserPerGroup(AssignUserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Invalid request data");
            }

            List<string> userIds = new List<string>();

            if (request.Type == "user")
            {
                // Nếu là user thì lấy trực tiếp các userId từ request
                userIds = request.Id;
            }
            else if (request.Type == "group")
            {
                // Nếu là group thì lấy tất cả các userId từ bảng UserPerResGroup
                userIds = await _context.UserPerResGroups
                    .Where(ur => request.Id.Contains(ur.ResponsiableGroupId))
                    .Select(ur => ur.UserId)
                    .Distinct()
                    .ToListAsync();
            }
            else
            {
                throw new ArgumentException("Invalid type");
            }

            // Lấy danh sách các userId đã có trong bảng UserPerTag với cùng TagId
            var existingUserIds = await _context.UserPerTags
                .Where(ut => ut.TagId == request.TagId)
                .Select(ut => ut.UserId)
                .ToListAsync();

            // Lọc ra những userId chưa được gán vào tag
            var newUserIds = userIds.Except(existingUserIds).ToList();

            if (newUserIds.Count == 0)
            {
                return;
            }

            // Lưu các userId chưa bị trùng lặp vào bảng UserPerTag
            foreach (var userId in newUserIds)
            {
                var userPerTag = new UserPerTag
                {
                    Id = Guid.NewGuid().ToString(),
                    TagId = request.TagId,
                    UserId = userId,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                _context.UserPerTags.Add(userPerTag);
            }

            await _context.SaveChangesAsync();
        }
    }
}
