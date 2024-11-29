using backend.Constants;
using Project.Entities;
using System.Data.Entity;

namespace Project.Service.User
{
    public class UserService : IUserService
    {


        private readonly HcmUeQTTB_DevContext _context;
        private readonly IConfiguration _config;
        private readonly string _baseURL;
        public UserService(HcmUeQTTB_DevContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
            _baseURL = _config.GetSection("AuthValidateUrl").Value;
        }

        public async Task<bool> VerifyAccess(string userId, string[] permissions)
        {
            return await _context.UserRoles.AnyAsync(u => u.UserId == userId &&
            (u.RoleId == RoleDefault.Administrator || u.Role.PermissionRoleMappings.Any(p => permissions.Any(value => value == p.Permission.Name))));
        }

        public async Task<bool> VerifyAsync(string token, string[] permissions)
        {
            string url = $"{_baseURL}/user/verify?";
            string queryString = "";
            foreach (var permission in permissions)
            {
                queryString += $"permissions={permission}&";
            }
            queryString = queryString.Remove(queryString.Length - 1);

            using var http = new HttpClient();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(url + queryString);
            request.Headers.Add("Authorization", token);
            HttpResponseMessage response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return Convert.ToBoolean(await response.Content.ReadAsStringAsync());

        }
    }
}
