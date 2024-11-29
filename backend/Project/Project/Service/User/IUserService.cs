namespace Project.Service.User
{
    public interface IUserService
    {
        Task<bool> VerifyAccess(string userId, string[] permissions);
        Task<bool> VerifyAsync(string token, string[] permissions);
    }
}
