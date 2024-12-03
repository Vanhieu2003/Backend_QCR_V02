using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class ResponsibleGroupRepository : IResponsibleGroupRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public ResponsibleGroupRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<List<ResponsibleGroup>> GetAllResponsiableGroup()
        {

            var group = await _context.ResponsibleGroups.ToListAsync();
            return group;
        }

        public async Task<ResponsiableGroupDto> GetAllResponsiableGroupById(string id)
        {

            var responsibleGroupData = await (from rg in _context.ResponsibleGroups
                                              join ubrg in _context.UserPerResGroups on rg.Id equals ubrg.ResponsiableGroupId into userGroup
                                              from ubrg in userGroup.DefaultIfEmpty()
                                              join u in _context.Users on ubrg.UserId equals u.Id into users
                                              from u in users.DefaultIfEmpty()
                                              where rg.Id == id
                                              select new
                                              {
                                                  ResponsibleGroup = rg,
                                                  User = u
                                              }).ToListAsync();

            if (responsibleGroupData == null || !responsibleGroupData.Any())
            {
                return null;
            }


            var responsibleGroupDto = new ResponsiableGroupDto
            {
                Id = responsibleGroupData.First().ResponsibleGroup.Id,
                GroupName = responsibleGroupData.First().ResponsibleGroup.GroupName,
                Description = responsibleGroupData.First().ResponsibleGroup.Description ?? "Không có mô tả",
                Color = responsibleGroupData.First().ResponsibleGroup.Color,
                Users = new List<UserDto>()
            };

            // Duyệt qua dữ liệu và thêm người dùng vào danh sách nếu có
            foreach (var data in responsibleGroupData)
            {
                if (data.User != null)
                {
                    var userDto = new UserDto
                    {
                        Id = data.User.Id,
                        FirstName = data.User.FirstName,
                        LastName = data.User.LastName,
                        UserName = data.User.UserName,
                        Email = data.User.Email
                    };
                    responsibleGroupDto.Users.Add(userDto);
                }
            }

            return responsibleGroupDto;
        }


        public async Task<List<ResponsiableGroupViewDto>> GetAll()
        {

            


            var responsiableGroups = await (from rg in _context.ResponsibleGroups
                                            join ubrg in _context.UserPerResGroups on rg.Id equals ubrg.ResponsiableGroupId into userGroup
                                            from ubrg in userGroup.DefaultIfEmpty()
                                            join u in _context.Users on ubrg.UserId equals u.Id into userList
                                            from u in userList.DefaultIfEmpty()
                                            group u by new
                                            {
                                                rg.Id,
                                                rg.GroupName,
                                                rg.Description,
                                                rg.Color
                                            } into g
                                            select new ResponsiableGroupViewDto
                                            {
                                                Id = g.Key.Id,
                                                GroupName = g.Key.GroupName,
                                                Description = g.Key.Description ?? "Không có mô tả",
                                                Color = g.Key.Color,
                                                NumberOfUser = g.Count(x => x != null)
                                            })
                                            .OrderBy(g => g.GroupName)
                                            .ToListAsync();


            return responsiableGroups;
        }


    }
}
