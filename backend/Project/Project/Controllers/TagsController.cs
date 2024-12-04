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
    public class TagsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly ITagRepository _repo;

        public TagsController(HcmUeQTTB_DevContext context, ITagRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Tags
        [HttpGet]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            if (_context.Tags == null)
            {
                return NotFound();
            }
            return await _context.Tags.ToListAsync();
        }

        [HttpGet("GetTagGroups")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetTagGroupsWithUserCount()
        {
            var tagGroups = await _repo.GetTagGroupsWithUserCountAsync();
           

            return Ok(tagGroups);
        }


        [HttpGet("GetGroupInfoByTagId")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetGroupInfoByTagId([FromQuery] string tagId)
        {
            var result = await _repo.GetGroupInfoByTagId(tagId);
            return Ok(result);
        }

        // GET: api/Tags
        [HttpGet("GetTag")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<ActionResult<Tag>> GetTag([FromQuery] string id)
        {
            if (_context.Tags == null)
            {
                return NotFound();
            }
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        [HttpGet("tagspercriteria")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetCriteriaTagByTag([FromQuery] string tagId)
        {
            var tags = await _repo.GetTagsPerCriteriaByTag(tagId);
            if (tags == null)
            {
                return NotFound();
            }
            return Ok(tags);
        }

        [HttpGet("average-ratings")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetTagAverageRatings()
        {
            var result = await _repo.GetTagAverageRatingsAsync();
            return Ok(result);
        }

        

       

      


    }
}
