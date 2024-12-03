using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Interface;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IRoomRepository _repo;

        public RoomsController(HcmUeQTTB_DevContext context, IRoomRepository repo)
        {
            _context = context;
            _repo = repo;
        }
        // GET: api/Rooms?id=5
        [HttpGet]
        public async Task<ActionResult<Room>> GetRoom([FromQuery] string id)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }


        [HttpGet("All")]
        public async Task<IActionResult> GetAllRooms([FromQuery] int pageSize = 50)
        {
            var rooms = await _context.Rooms
                .Take(pageSize) 
                .Select(r => new
                {
                    r.Id,
                    r.RoomName,
                    r.FloorId,
                    r.BlockId,
                    r.RoomCategoryId
                })
                .ToListAsync();

            if (rooms == null || !rooms.Any())
            {
                return NotFound();
            }

            return Ok(rooms);
        }




        [HttpGet("By-Floor&Block")]
        public async Task<IActionResult> GetRoomsByFloorIdAndBlockId([FromQuery] string floorId,
     [FromQuery] string blockId)
        {
            var rooms = await _repo.GetRoomByFloorIdAndBlockId(floorId, blockId);

            if (rooms == null || rooms.Count == 0)
            {
                return NotFound();
            }

            return Ok(rooms);
        }

        [HttpGet("IfExistForm-Floor&Block")]
        public async Task<IActionResult> GetRoomsByFloorIdAndBlockIdIfFormExists([FromQuery] string floorId,
     [FromQuery] string blockId)
        {
            var rooms = await _repo.GetRoomByFloorIdAndBlockIdIfFormExist(floorId, blockId);

            if (rooms == null || rooms.Count == 0)
            {
                return NotFound();
            }

            return Ok(rooms);
        }
        // search Roomname
        [HttpGet("SearchRoom")]
        public async Task<IActionResult> SearchRoom([FromQuery] string roomName)
        {
            var rooms = await _repo.SearchRoom(roomName);
            return Ok(rooms);

        }

        [HttpGet("GetRoomByBlocksAndCampus")]
        public async Task<IActionResult> GetRoomsByBlockAndCampus([FromQuery] string blockId, [FromQuery] string campusId)
        {

            if (string.IsNullOrEmpty(blockId) || string.IsNullOrEmpty(campusId))
            {
                return BadRequest("BlockId và CampusId không được bỏ trống.");
            }


            var rooms = await _repo.GetRoomsByBlockAndCampusAsync(blockId, campusId);

            if (rooms == null || rooms.Count == 0)
            {
                return NotFound("Không tìm thấy phòng nào cho block và campus được chỉ định.");
            }


            return Ok(rooms);
        }

        [HttpGet("GetRoomByCampus")]
        public async Task<IActionResult> GetRoomsByCampusAsync([FromQuery] string campusId)
        {

            if (string.IsNullOrEmpty(campusId))
            {
                return BadRequest("CampusId không được bỏ trống.");
            }


            var rooms = await _repo.GetRoomsByCampusAsync(campusId);

            if (rooms == null || rooms.Count == 0)
            {
                return NotFound("Không tìm thấy phòng nào cho campus được chỉ định.");
            }


            return Ok(rooms);
        }
    }
}
