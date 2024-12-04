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
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriteriasPerFormsController : ControllerBase
    {
        private readonly HcmUeQTTB_DevContext _context;
        private readonly ICriteriasPerFormRepository _repo;

        public CriteriasPerFormsController(HcmUeQTTB_DevContext context, ICriteriasPerFormRepository repo)
        {
            _context = context;
            _repo = repo;
        }

    
        [HttpGet("ByFormId")]
        [ClaimPermission(PermissionConstants.ViewForm)]
        public async Task<IActionResult> GetCriteriaByFormId([FromQuery] string formId)
        {
            var criteria = await _repo.GetCriteriaByFormId(formId);

            if (criteria == null || !criteria.Any())
            {
                return NotFound();
            }

            return Ok(criteria);
        }


        [HttpPut("edit")]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<ActionResult> EditForm([FromBody] EditFormDto formData)
        {
            // Tìm form hiện tại
            var existingForm = await _context.CleaningForms.FindAsync(formData.FormId);
            if (existingForm == null)
            {
                return NotFound("Form không tồn tại.");
            }

            // Xóa các CriteriaPerForm cũ liên quan đến formId
            var oldCriteria = _context.CriteriasPerForms.Where(cpf => cpf.FormId == formData.FormId);
            _context.CriteriasPerForms.RemoveRange(oldCriteria);

            // Thêm lại các CriteriaPerForm mới
            foreach (var criteria in formData.CriteriaList)
            {
                var newCriteriaPerForm = new CriteriasPerForm
                {
                    Id = Guid.NewGuid().ToString(),
                    FormId = formData.FormId,
                    CriteriaId = criteria.Id,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                _context.CriteriasPerForms.Add(newCriteriaPerForm);
            }

            await _context.SaveChangesAsync();
            return Ok("Form cập nhật thành công.");
        }

       
        [HttpPost("newForm")]
        [ClaimPermission(PermissionConstants.ModifyForm)]
        public async Task<ActionResult> AddTagsForCriteria([FromBody] CriteriaPerFormDto newForm)
        {
            foreach (var criteria in newForm.criteriaList)
            {
                var newCriteriaPerForm = new CriteriasPerForm
                {
                    Id = Guid.NewGuid().ToString(),
                    FormId = newForm.formId,
                    CriteriaId = criteria.Id,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                _context.CriteriasPerForms.Add(newCriteriaPerForm);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

       
    }
}
