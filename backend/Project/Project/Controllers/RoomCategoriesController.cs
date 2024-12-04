using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Constants;
using backend.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Interface;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomCategoriesController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IRoomCategoryRepository _repo;

        public RoomCategoriesController(HcmUeQTTB_DevContext context, IRoomCategoryRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/RoomCategories
        [HttpGet]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<IEnumerable<RoomCategory>>> GetRoomCategories()
        {
            var category = from rc in _context.RoomCategories
                           select new
                           {
                               rc.Id,
                               rc.CategoryName
                           };

            return Ok(await category.ToListAsync());
        }

        // GET: api/RoomCategories/5
        [HttpGet("id")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<RoomCategory>> GetRoomCategory([FromQuery] string id)
        {
            if (_context.RoomCategories == null)
            {
                return NotFound();
            }
            var roomCategory = await _context.RoomCategories.FindAsync(id);

            if (roomCategory == null)
            {
                return NotFound();
            }

            return roomCategory;
        }
        //lấy theo criteriaId
        [HttpGet("criteria")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<RoomCategory>> GetRoomCategoriesbyCriteriaId([FromQuery] string criteriaId)
        {
            var roomCategory = await _repo.GetRoomCategoriesbyCriteriaId(criteriaId);

            if (roomCategory == null)
            {
                return NotFound();
            }

            return Ok(roomCategory);
        }

     
    }
}
