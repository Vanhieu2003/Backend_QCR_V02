using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriteriaController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly ICriteriaRepository _repo;

        public CriteriaController(HcmUeQTTB_DevContext context, ICriteriaRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Criteria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criteria>>> GetCriteria([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {


            if (pageNumber < 1)
            {
                return BadRequest("Số trang không hợp lệ.");
            }

            if (pageSize <= 0)
            {
                return BadRequest("Kích thước trang không hợp lệ.");
            }

            var criterias = await _repo.GetAllCriteria(pageNumber, pageSize);
            var totalValue = await _context.Criteria.CountAsync(c => c.Status == "ENABLE");

            if (criterias == null || !criterias.Any())
            {
                return NotFound("Không tìm thấy tiêu chí.");
            }
            var response = new { criterias, totalValue };
            return Ok(response);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<Criteria>> GetAllCriteria()
        {
            if (_context.Criteria == null)
            {
                return NotFound();
            }
            var criteria = await _context.Criteria.Where(c => c.Status == "ENABLE").ToListAsync();

            if (criteria == null)
            {
                return NotFound();
            }

            return Ok(criteria);
        }

      

        [HttpGet("ByRoom")]
        public async Task<IActionResult> GetCriteriasByRoomCategoricalId([FromQuery] string RoomCategoricalId)
        {
            var criteriaList = await _repo.GetCriteriasByRoomsCategoricalId(RoomCategoricalId);
            if (criteriaList == null)
            {
                return NotFound();
            }
            return Ok(criteriaList);
        }
        [HttpGet("ByRoomId")]
        public async Task<IActionResult> GetCriteriasByRoomId([FromQuery] string RoomId)
        {
            var criteriaList = await _repo.GetCriteriasByRoomId(RoomId);
            if (criteriaList == null)
            {
                return NotFound();
            }
            return Ok(criteriaList);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCriteria([FromQuery] string keyword)
        {
            // Execute search using repository
            var criteria = await _repo.SearchCriteria(keyword);
            return Ok(criteria);
        }




        [HttpPut]
        public async Task<IActionResult> DisableCriteria([FromQuery] string id)
        {
            // Kiểm tra nếu _context.Criteria null
            if (_context.Criteria == null)
            {
                return NotFound("Criteria table is not available.");
            }

            // Tìm kiếm criteria theo id
            var criteria = await _context.Criteria.FindAsync(id);
            if (criteria == null)
            {
                return NotFound($"Criteria with ID '{id}' not found.");
            }

            // Cập nhật cột Status thành "DISABLE"
            criteria.Status = "DISABLE";

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Criteria.Update(criteria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("CreateCriteria")]
        public async Task<IActionResult> CreateCriteria([FromBody] CreateCriteriaDto criteriaDto)
        {
            try
            {
                var newCriteria = await _repo.CreateCriteria(criteriaDto);
                return Ok(newCriteria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

       
    }
}
