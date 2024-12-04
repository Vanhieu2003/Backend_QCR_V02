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

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IFloorRepository _repo;

        public FloorsController(HcmUeQTTB_DevContext context, IFloorRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // API lấy Floor theo BlockId, dùng query parameter
        [HttpGet("Block")]
        [ClaimPermission(PermissionConstants.AccessQcr)]
        public async Task<IActionResult> GetFloorByBlockId([FromQuery] string BlockId)
        {
            var floor = await _repo.GetFloorByBlockId(BlockId);
            if (floor == null)
            {
                return NotFound();
            }
            return Ok(floor);
        }

        // API lấy Floor theo id, dùng query parameter
        [HttpGet]
        [ClaimPermission(PermissionConstants.AccessQcr)]
        public async Task<ActionResult<Floor>> GetFloor([FromQuery] string id)
        {
            if (_context.Floors == null)
            {
                return NotFound();
            }
            var floor = await _context.Floors.FindAsync(id);

            if (floor == null)
            {
                return NotFound();
            }

            return floor;
        }
    }
}
