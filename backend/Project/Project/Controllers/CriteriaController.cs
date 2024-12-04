using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using backend.Constants;
using backend.Filters;
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

     
        [HttpGet("GetAll")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<IEnumerable<Criteria>>> GetCriteria()
        {
            var criterias = await _repo.GetAllCriteria();
       
            return Ok(criterias);
        }
      

        [HttpGet("ByRoom")]
        [ClaimPermission(PermissionConstants.ViewForm)]
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
        [ClaimPermission(PermissionConstants.ViewForm)]
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
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> SearchCriteria([FromQuery] string keyword)
        {
            // Execute search using repository
            var criteria = await _repo.SearchCriteria(keyword);
            return Ok(criteria);
        }




        [HttpPut]
        [ClaimPermission(PermissionConstants.ModifyForm)]
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
        [ClaimPermission(PermissionConstants.ModifyForm)]
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
