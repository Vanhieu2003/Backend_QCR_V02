using backend.Constants;
using backend.Filters;
using Microsoft.AspNetCore.Mvc;
using Project.Dto;
using Project.Entities;
using Project.Interface;
using Project.Repository;

namespace Project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class QRController : Controller
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IQRScannerRepository _repo;

        public QRController(HcmUeQTTB_DevContext context, IQRScannerRepository repo)
        {
            _context = context;
            _repo = repo;

        }

        [HttpGet("GetInfo")]
        [ClaimPermission(PermissionConstants.ModifyReport)]
        public async Task<IActionResult> getInfoByQR([FromQuery] string roomCode)
        {
            try
            {
                var roomInfo = await _repo.getInfoByRoomCode(roomCode);
                return Ok(roomInfo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi. Vui lòng thử lại sau." });
            }
        }
    }
}
