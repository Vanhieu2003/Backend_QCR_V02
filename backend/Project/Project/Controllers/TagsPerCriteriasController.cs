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
    public class TagsPerCriteriasController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly ITagsPerCriteriaRepository _repo;

        public TagsPerCriteriasController(HcmUeQTTB_DevContext context, ITagsPerCriteriaRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/TagsPerCriterias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagsPerCriteria>>> GetTagsPerCriteria()
        {
            if (_context.TagsPerCriteria == null)
            {
                return NotFound();
            }
            return await _context.TagsPerCriteria.ToListAsync();
        }

        // GET: api/TagsPerCriterias
        [HttpGet("id")]
        public async Task<ActionResult<TagsPerCriteria>> GetTagsPerCriteria([FromQuery] string id)
        {
            if (_context.TagsPerCriteria == null)
            {
                return NotFound();
            }
            var tagsPerCriteria = await _context.TagsPerCriteria.FindAsync(id);

            if (tagsPerCriteria == null)
            {
                return NotFound();
            }

            return tagsPerCriteria;
        }

        [HttpGet("criteria")]
        public async Task<IActionResult> GetTagsByCriteriaId([FromQuery] string criteriaId)
        {
            var tags = await _repo.GetTagsByCriteriaId(criteriaId);

            if (tags == null || !tags.Any())
            {
                return NotFound();
            }

            return Ok(tags);
        }

        // PUT: api/TagsPerCriterias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutTagsPerCriteria([FromQuery] string id, [FromBody] TagsPerCriteria tagsPerCriteria)
        {
            if (id != tagsPerCriteria.Id)
            {
                return BadRequest();
            }

            _context.Entry(tagsPerCriteria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagsPerCriteriaExists(id))
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

        // POST: api/TagsPerCriterias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagsPerCriteria>> PostTagsPerCriteria([FromBody] TagsPerCriteria tagsPerCriteria)
        {
            if (_context.TagsPerCriteria == null)
            {
                return Problem("Entity set 'HcmUeQTTB_DevContext.TagsPerCriteria' is null.");
            }

            if (string.IsNullOrEmpty(tagsPerCriteria.Id))
            {
                tagsPerCriteria.Id = Guid.NewGuid().ToString();
            }

            _context.TagsPerCriteria.Add(tagsPerCriteria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TagsPerCriteriaExists(tagsPerCriteria.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetTagsPerCriteria), new { id = tagsPerCriteria.Id }, tagsPerCriteria);
        }

        [HttpPost("newCriteria")]
        public async Task<ActionResult> AddTagsForCriteria([FromBody] CriteriaDto newCriteria)
        {
            foreach (var tag in newCriteria.Tag)
            {
                var newTagPerCriteria = new TagsPerCriteria
                {
                    Id = Guid.NewGuid().ToString(),
                    CriteriaId = newCriteria.CriteriaId,
                    TagId = tag.Id,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                _context.TagsPerCriteria.Add(newTagPerCriteria);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/TagsPerCriterias
        [HttpDelete]
        public async Task<IActionResult> DeleteTagsPerCriteria([FromQuery] string id)
        {
            if (_context.TagsPerCriteria == null)
            {
                return NotFound();
            }
            var tagsPerCriteria = await _context.TagsPerCriteria.FindAsync(id);
            if (tagsPerCriteria == null)
            {
                return NotFound();
            }

            _context.TagsPerCriteria.Remove(tagsPerCriteria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagsPerCriteriaExists(string id)
        {
            return (_context.TagsPerCriteria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
