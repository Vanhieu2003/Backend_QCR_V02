using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CriteriaReportsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly ICriteriaReportRepository _repo;
        public CriteriaReportsController(HcmUeQTTB_DevContext context, ICriteriaReportRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/CriteriaReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriteriaReport>>> GetCriteriaReports()
        {
            if (_context.CriteriaReports == null)
            {
                return NotFound();
            }
            return await _context.CriteriaReports.ToListAsync();
        }

        // GET: api/CriteriaReports/5
        [HttpGet("id")]
        public async Task<ActionResult<CriteriaReport>> GetCriteriaReport([FromQuery] string id)
        {
            if (_context.CriteriaReports == null)
            {
                return NotFound();
            }
            var criteriaReport = await _context.CriteriaReports.FindAsync(id);

            if (criteriaReport == null)
            {
                return NotFound();
            }

            return criteriaReport;
        }

        [HttpGet("Criteria")]
        public async Task<IActionResult> GetReportByCriteriaId([FromQuery] string criteriaId)
        {
            var reports = await _repo.GetReportByCriteriaId(criteriaId);

            if (reports == null || !reports.Any())
            {
                return NotFound();
            }

            return Ok(reports);
        }


       

        [HttpPost]
        public async Task<ActionResult<CriteriaReport>> PostCriteriaReport([FromBody] CriteriaReport criteriaReport)
        {
            
            var existingReport = await _repo.GetExistingReportAsync(criteriaReport);
            if (existingReport != null)
            {
                return Conflict(new { message = "Dữ liệu không hợp lệ" });
            }

           
            if (string.IsNullOrEmpty(criteriaReport.Id))
            {
                criteriaReport.Id = Guid.NewGuid().ToString();
            }

            try
            {
               
                await _repo.AddCriteriaReportAsync(criteriaReport);
            }
            catch (DbUpdateException)
            {
                    throw;  
            }

            return Ok(criteriaReport);

        }





        
    }
}
