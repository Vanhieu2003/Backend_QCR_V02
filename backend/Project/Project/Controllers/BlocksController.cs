using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.dto;
using Project.Dto;
using Project.Entities;
using Project.Filters;
using Project.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IBlockRepository _repo;

        public BlocksController(HcmUeQTTB_DevContext context, IBlockRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<BlockDto>>> GetBlocks()
        {
            var blocks = from b in _context.Blocks
                         select new
                         {
                             b.Id,
                             b.BlockName,
                             b.CampusId,
                             b.CampusName,
                             b.SortOrder
                         };

            var sortedBlocks = blocks.OrderBy(b => b.SortOrder)
                                     .Select(b => new BlockDto
                                     {
                                         Id = b.Id,
                                         BlockName = b.BlockName,
                                         CampusId = b.CampusId,
                                         CampusName = b.CampusName
                                     });

            return Ok(await sortedBlocks.ToListAsync());
        }

        // Định nghĩa route theo dạng ByCampus/campusId và nhận campusId từ query string
        [HttpGet("ByCampus")]
        public async Task<IActionResult> GetBlocksByCampusId([FromQuery] string campusId)
        {
            if (string.IsNullOrEmpty(campusId))
            {
                return BadRequest("CampusId là bắt buộc.");
            }

            var blocks = await _repo.GetBlocksByCampusIdAsync(campusId);
            if (blocks == null || !blocks.Any())
            {
                return NotFound();
            }
            return Ok(blocks);
        }

        // GET: api/Blocks/5
        [HttpGet("id")]
        public async Task<ActionResult<Block>> GetBlock([FromQuery] string id)
        {
            if (_context.Blocks == null)
            {
                return NotFound();
            }
            var block = await _context.Blocks.FindAsync(id);

            if (block == null)
            {
                return NotFound();
            }

            return block;
        }
    }
}
