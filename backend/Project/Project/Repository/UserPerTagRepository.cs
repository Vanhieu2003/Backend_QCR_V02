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
                    CreateAt = GetCurrentTimeInGmtPlus7(),
                    UpdateAt = GetCurrentTimeInGmtPlus7()
                };

                _context.UserPerTags.Add(userPerTag);
            }

            await _context.SaveChangesAsync();
        }


        public async Task UpdateUsersForTag(UpdateUserRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Id) || request.Users == null || !request.Users.Any())
            {
                throw new ArgumentException("Invalid request data");
            }

            // Lấy danh sách các userId đã tồn tại trong bảng UserPerTag với TagId
            var existingUserIds = await _context.UserPerTags
                .Where(ut => ut.TagId == request.Id)
                .Select(ut => ut.UserId)
                .ToListAsync();

            // Lọc danh sách người dùng mới (chỉ thêm user chưa tồn tại)
            var newUserIds = request.Users.Except(existingUserIds).ToList();

            // Lọc danh sách người dùng cần xóa (tồn tại trong database nhưng không có trong danh sách mới)
            var userIdsToDelete = existingUserIds.Except(request.Users).ToList();

            // Thêm các user mới vào cơ sở dữ liệu
            if (newUserIds.Any())
            {
                var newUserPerTags = newUserIds.Select(userId => new UserPerTag
                {
                    Id = Guid.NewGuid().ToString(),
                    TagId = request.Id,
                    UserId = userId,
                    CreateAt = GetCurrentTimeInGmtPlus7(),
                    UpdateAt = GetCurrentTimeInGmtPlus7()
                }).ToList();

                await _context.UserPerTags.AddRangeAsync(newUserPerTags);
            }

            // Xóa các user không còn trong danh sách
            if (userIdsToDelete.Any())
            {
                var usersToDelete = await _context.UserPerTags
                    .Where(ut => ut.TagId == request.Id && userIdsToDelete.Contains(ut.UserId))
                    .ToListAsync();

                _context.UserPerTags.RemoveRange(usersToDelete);
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }


        private DateTime GetCurrentTimeInGmtPlus7()
        {

            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus7);
        }

    }
}
