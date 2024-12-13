using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;


namespace Project.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly HcmUeQTTB_DevContext _context;

        public ApplicationRepository (HcmUeQTTB_DevContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ApplicationDto>> GetApplicationsAsync()
        {
            var result = await (from a in _context.Application
                                join pe in _context.Permissions on a.Id equals pe.AppId
                                select new ApplicationDto
                                {
                                    AppName = a.AppName,
                                    Logo = a.Logo,
                                    Permission = pe.Name
                                }).ToListAsync();
            return result;
        }
    }
}
