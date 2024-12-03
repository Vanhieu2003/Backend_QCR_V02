using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using Project.Dto;
using Project.Entities;
using Project.Interface;
using Project.Repository;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningReportsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly ICleaningReportRepository _repo;

        public CleaningReportsController(HcmUeQTTB_DevContext context, ICleaningReportRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/CleaningReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CleaningReport>>> GetCleaningReports()
        {
           
            return await _context.CleaningReports.ToListAsync();
        }

        // GET: api/CleaningReports/5
        [HttpGet("GetById")]
        public async Task<ActionResult<CleaningReportDetailsDto>> GetCleaningReport([FromQuery] string id)
        {
           
            var cleaningReport = await _repo.GetInfoByReportId(id);
            return cleaningReport;
        }

        // GET: api/CleaningReports/ByCleaningForm
        [HttpGet("GetByCleaningForm")]
        public async Task<IActionResult> GetCleaningReportByCleaningForm([FromQuery] string formId)
        {
            var reports = await _repo.GetCleaningReportByCleaningForm(formId);
            return Ok(reports);
        }

        [HttpGet("GetAllInfo")]
        public async Task<IActionResult> GetAllCleaningReport()
        {
          
            var result = await _repo.GetReportInfo();
           
            return Ok(result);
        }

        [HttpGet("GetAllInfoByUserId")]
        public async Task<IActionResult> GetCleaningReportByUserId(string userId)
        {

            var result = await _repo.GetReportInfoByUserId(userId);

            return Ok(result);
        }


        [HttpGet("GetAllInfoByManagerId")]
        public async Task<IActionResult> GetCleaningReportByManagerId(string managerId)
        {

            var result = await _repo.GetReportInfoByManagerId(managerId);

            return Ok(result);
        }



            [HttpGet("GetReportInfo")]
        public async Task<IActionResult> GetReportInfoByReportId([FromQuery] string reportId)
        {
            var report = await _repo.GetInfoByReportId(reportId);
            return Ok(report);
        }


        [HttpGet("GetFullInfo")]
        public async Task<ActionResult> GetReportDetails([FromQuery] string reportId)
        {
            
                var result = await _repo.GetReportDetails(reportId);        
                return Ok(result);
           
        }



        [HttpPut("update")]
        public async Task<IActionResult> UpdateCriteriaAndCleaningReport([FromBody] UpdateCleaningReportRequest request)
        {
            try
            {
                var updatedReport = await _repo.UpdateCriteriaAndCleaningReport(request);
                return Ok(updatedReport);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<CleaningReport>> CreateCleaningReport([FromBody] CleaningReportRequest request)
        {         
            var cleaningReport = await _repo.CreateCleaningReportAsync(request);
            return Ok(cleaningReport);
        }

        [HttpPost("user-score")]
        public async Task<IActionResult> Evaluate([FromBody] EvaluationRequest request)
        {
            var userScores = await _repo.EvaluateUserScores(request);
            return Ok(userScores);
        }






    }
}
