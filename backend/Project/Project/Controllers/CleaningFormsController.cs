using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Constants;
using backend.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningFormsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly ICleaningFormRepository _repo;

        public CleaningFormsController(HcmUeQTTB_DevContext context, ICleaningFormRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/CleaningForms
        [HttpGet]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<IEnumerable<object>>> GetCleaningForms()
        {
            var results = await (from cf in _context.CleaningForms
                                join r in _context.Rooms on cf.RoomId equals r.Id
                                join f in _context.Floors on r.FloorId equals f.Id
                                join b in _context.Blocks on r.BlockId equals b.Id
                                join c in _context.Campuses on b.CampusId equals c.Id
                                orderby c.SortOrder
                                select new
                                {
                                    id = cf.Id,
                                    formName = cf.FormName,
                                    CampusName = c.CampusName,
                                    BlockName = b.BlockName,
                                    FloorName = f.FloorName,
                                    RoomName = r.RoomName
                                })
                        
                        .ToListAsync();

            
            
            return Ok(results);
        }

        // GET: api/CleaningForms?id={id}
        [HttpGet("id")]
        public async Task<ActionResult<CleaningForm>> GetCleaningForm([FromQuery] string id)
        {
            if (_context.CleaningForms == null)
            {
                return NotFound();
            }
            var cleaningForm = await _context.CleaningForms.FindAsync(id);
            if (cleaningForm == null)
            {
                return NotFound();
            }
            return cleaningForm;
        }

        // GET: api/CleaningForms/GetFullInfo?formId={formId}
        [HttpGet("GetFullInfo")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult> GetFormDetails([FromQuery] string formId)
        {
            var form = await _context.CleaningForms.FirstOrDefaultAsync(f => f.Id == formId);
            if (form == null)
            {
                return NotFound("Form không tồn tại");
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == form.RoomId);
            if (room == null)
            {
                return NotFound("Phòng không tồn tại");
            }

            var block = await _context.Blocks.FirstOrDefaultAsync(b => b.Id == room.BlockId);
            if (block == null)
            {
                return NotFound("Block không tồn tại");
            }

            var campus = await _context.Campuses.FirstOrDefaultAsync(c => c.Id == block.CampusId);
            if (campus == null)
            {
                return NotFound("Campus không tồn tại");
            }

            var floor = await _context.Floors.FirstOrDefaultAsync(f => f.Id == room.FloorId);
            if (floor == null)
            {
                return NotFound("Tầng không tồn tại");
            }

            var criteriaPerForm = await _context.CriteriasPerForms
                .Where(cpf => cpf.FormId == formId)
                .ToListAsync();

            var criteriaList = new List<object>();
            foreach (var cpf in criteriaPerForm)
            {
                var criteria = await _context.Criteria.FirstOrDefaultAsync(c => c.Id == cpf.CriteriaId);
                if (criteria != null)
                {
                    var tagIds = await _context.TagsPerCriteria
                        .Where(tpc => tpc.CriteriaId == criteria.Id)
                        .Select(tpc => tpc.TagId)
                        .ToListAsync();

                    var tags = await _context.Tags
                        .Where(tag => tagIds.Contains(tag.Id))
                        .Select(tag => new
                        {
                            Id = tag.Id,
                            Name = tag.TagName
                        })
                        .ToListAsync();

                    criteriaList.Add(new
                    {
                        Id = criteria.Id,
                        Name = criteria.CriteriaName,
                        CriteriaType = criteria.CriteriaType,
                        Tags = tags
                    });
                }
            }

            var result = new
            {
                IdForm = form.Id,
                CampusName = campus?.CampusName,
                BlockName = block?.BlockName,
                FloorName = floor?.FloorName,
                RoomName = room?.RoomName,
                CriteriaList = criteriaList
            };

            return Ok(result);
        }

        // GET: api/CleaningForms/criteria?formId={formId}
        [HttpGet("criteria")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetCriteriaByFormId([FromQuery] string formId)
        {
            var criteria = await _repo.GetCriteriaByFormId(formId);
            if (criteria == null)
            {
                return NotFound();
            }
            return Ok(criteria);
        }

        // GET: api/CleaningForms/ByRoomId?RoomId={RoomId}
        [HttpGet("ByRoomId")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetFormByRoomId([FromQuery] string RoomId)
        {
            var form = await _repo.GetCleaningFormByRoomId(RoomId);
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }

        // PUT: api/CleaningForms?id={id}
        [HttpPut]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<IActionResult> PutCleaningForm([FromQuery] string id, [FromBody] CleaningForm cleaningForm)
        {
            if (id != cleaningForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(cleaningForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleaningFormExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CleaningForms
        [HttpPost]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<ActionResult<CleaningForm>> PostCleaningForm([FromBody] CleaningForm cleaningForm)
        {
            if (_context.CleaningForms == null)
            {
                return Problem("Entity set 'HcmUeQTTB_DevContext.CleaningForms' is null.");
            }

            var existingForm = await _context.CleaningForms
                .FirstOrDefaultAsync(cf => cf.RoomId == cleaningForm.RoomId);

            if (existingForm != null)
            {
                return Conflict(new { message = "Room ID already exists in Cleaning Forms." });
            }

            if (string.IsNullOrEmpty(cleaningForm.Id))
            {
                cleaningForm.Id = Guid.NewGuid().ToString();
            }

            _context.CleaningForms.Add(cleaningForm);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CleaningFormExists(cleaningForm.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostCleaningForm", new { id = cleaningForm.Id }, cleaningForm);
        }

        [HttpPost("create-form")]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<IActionResult> CreateCleaningForms([FromBody] CreateCleaningFormRequest request)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                foreach (var room in request.RoomId)
                {
                    var cleaningForm = new CleaningForm
                    {
                        Id = Guid.NewGuid().ToString(),
                        FormName = request.FormName,
                        RoomId = room.Id,
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow,
                    };

                    await _context.CleaningForms.AddAsync(cleaningForm);
                    await _context.SaveChangesAsync();

                    foreach (var criteria in request.CriteriaList)
                    {
                        var criteriaPerForm = new CriteriasPerForm
                        {
                            Id = Guid.NewGuid().ToString(),
                            FormId = cleaningForm.Id,
                            CriteriaId = criteria.Id,
                            CreateAt = DateTime.UtcNow,
                            UpdateAt = DateTime.UtcNow
                        };

                        await _context.CriteriasPerForms.AddAsync(criteriaPerForm);
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return Ok(new { Message = "Tạo thành công các form", TotalFormsCreated = request.RoomId.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

        private bool CleaningFormExists(string id)
        {
            return (_context.CleaningForms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
