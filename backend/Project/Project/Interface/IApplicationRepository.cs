using Project.Dto;

namespace Project.Interface
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<ApplicationDto>> GetApplicationsAsync();
    }
}
