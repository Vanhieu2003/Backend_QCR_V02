using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Constants;
using backend.Filters;
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
        [ClaimPermission(PermissionConstants.ViewReport)]
        public async Task<ActionResult<IEnumerable<CleaningReport>>> GetCleaningReports()
        {
           
            return await _context.CleaningReports.ToListAsync();
        }

        // GET: api/CleaningReports/5
        [HttpGet("GetById")]
        [ClaimPermission(PermissionConstants.ViewReport)]
        public async Task<ActionResult<CleaningReportDetailsDto>> GetCleaningReport([FromQuery] string id)
        {
           
            var cleaningReport = await _repo.GetInfoByReportId(id);
            return cleaningReport;
        }

        // GET: api/CleaningReports/ByCleaningForm
        [HttpGet("GetByCleaningForm")]
        [ClaimPermission(PermissionConstants.ViewReport)]
        public async Task<IActionResult> GetCleaningReportByCleaningForm([FromQuery] string formId)
        {
            var reports = await _repo.GetCleaningReportByCleaningForm(formId);
            return Ok(reports);
        }

        [HttpGet("GetAllInfo")]
        [ClaimPermission(PermissionConstants.ViewReport)]
        public async Task<IActionResult> GetAllCleaningReport()
        {
          
            var result = await _repo.GetReportInfo();
           
            return Ok(result);
        }

        [HttpGet("GetAllInfoByUserId")]
        [ClaimPermission(PermissionConstants.ViewReport)]
        public async Task<IActionResult> GetCleaningReportByUserId(string userId)
        {

            var result = await _repo.GetReportInfoByUserId(userId);

            return Ok(result);
        }


        [HttpGet("GetAllInfoByManagerId")]
        [ClaimPermission(PermissionConstants.ViewReport)]
        public async Task<IActionResult> GetCleaningReportByManagerId(string managerId)
        {

            var result = await _repo.GetReportInfoByManagerId(managerId);

            return Ok(result);
        }





        [HttpGet("GetFullInfo")]
        [ClaimPermission(PermissionConstants.ViewReport)]
        public async Task<ActionResult> GetReportDetails([FromQuery] string reportId)
        {
            
                var result = await _repo.GetReportDetails(reportId);        
                return Ok(result);
           
        }



        [HttpPut("update")]
        [ClaimPermission(PermissionConstants.ModifyReport)]
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
        [ClaimPermission(PermissionConstants.ModifyReport)]
        public async Task<ActionResult<CleaningReport>> CreateCleaningReport([FromBody] CleaningReportRequest request)
        {         
            var cleaningReport = await _repo.CreateCleaningReportAsync(request);
            return Ok(cleaningReport);
        }

        [HttpPost("user-score")]
        [ClaimPermission(PermissionConstants.ModifyReport)]
        public async Task<IActionResult> Evaluate([FromBody] EvaluationRequest request)
        {
            var userScores = await _repo.EvaluateUserScores(request);
            return Ok(userScores);
        }






    }
}
