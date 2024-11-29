using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IChartRepository _repo;

        public ChartController(HcmUeQTTB_DevContext context, IChartRepository chartRepository)
        {
            _context = context;
            _repo = chartRepository;
        }
        
        
        [HttpGet("GetTopCriteriaValuesByCampus")]
        public async Task<IActionResult> GetTopCriteriaValuesByCampus([FromQuery] string? campusId)
        {
            var result = await _repo.GetTopCriteriaValuesByCampus(campusId);
            return Ok(result);
        }

        [HttpGet("GetCleaningReportsByQuarter")]
        public async Task<IActionResult> GetCleaningReportsByQuarter()
        {
            var result = await _repo.GetCleaningReportsByQuarter();
            return Ok(result);
        }

        // thêm giao diện
        [HttpGet("GetCleaningReportsByMonth")]
        public async Task<IActionResult> GetCleaningReportsByMonth(int? month = null, int? year = null)
        {

            var result = await _repo.GetCleaningReportsByMonth(month,year);
            return Ok(result);
        }





        //[HttpGet("GetBlockReports")]
        //public async Task<IActionResult> GetBlockReports([FromQuery] string campusId, [FromQuery] DateTime? targetDate = null)
        //{
        //    var result = await _repo.GetBlockReportsAsync(campusId, targetDate);
        //    return Ok(result);
        //}



       

        [HttpGet("comparison")]
        public async Task<IActionResult> GetCampusReportComparison(int? year = null)
        {
            var result = await _repo.GetCampusReportComparison(year);
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetCleaningReportSummary([FromQuery] string? campusId)
        {
            var result = await _repo.GetCleaningReportSummary(campusId);
            return Ok(result);
        }

        [HttpGet("responsible-tag-report")]
        public async Task<IActionResult> GetResponsibleTagReportByCampus([FromQuery] string? campusId)
        {
            var result = await _repo.GetResponsibleTagReportByCampus(campusId);
            return Ok(result);
        }

        [HttpGet("room-group-report")]
        public async Task<IActionResult> GetRoomGroupReportByCampus([FromQuery] string? campusId)
        {
            var result = await _repo.GetRoomGroupReportByCampus(campusId);
            return Ok(result);
        }

        [HttpGet("detail-report")]
        public async Task<IActionResult> GetCampusDetailReportById([FromQuery] string? campusId)
        {
            var result = await _repo.GetCampusDetailReportById(campusId);
            return Ok(result);
        }

        [HttpGet("GetShiftEvaluations")]
        public async Task<IActionResult> GetShiftEvaluations([FromQuery] string? campusId = null)
        {
            var evaluations = await _repo.GetShiftEvaluationsAsync(campusId);
            return Ok(evaluations);
        }

    }

}



