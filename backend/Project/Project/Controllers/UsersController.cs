using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;

        public UsersController(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var user = from u in _context.Users
                       select new UserDto
                       {
                           Id = u.Id,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           UserName = u.UserName,
                           Email = u.Email
                       };
            return await user.ToListAsync();
        }

        
      

      
    }
}
