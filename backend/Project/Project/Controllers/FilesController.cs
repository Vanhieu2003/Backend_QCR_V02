using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Project.Entities;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly HcmUeQTTB_DevContext _context;

        public FilesController(IWebHostEnvironment hostingEnvironment, HcmUeQTTB_DevContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // API để upload file và lưu URL vào bảng CriteriaReport
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not selected");
            }
            var result = await WriteFile(file);
            if (string.IsNullOrEmpty(result))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "File upload failed.");
            }

            // Tạo URL cho file
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{result}";


            return Ok(new { FileUrl = fileUrl });

        }

        [HttpPost]
        [Route("UploadFiles")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest("No files selected");
            }

            var fileUrls = new List<string>();

            foreach (var file in files)
            {
                var result = await WriteFile(file);
                if (string.IsNullOrEmpty(result))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "File upload failed.");
                }

                // Tạo URL cho file
                var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{result}";
                fileUrls.Add(fileUrl);
            }

            return Ok(new { FileUrls = fileUrls });
        }

        // API để tải file từ server và trả về file
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", filename);

            if (!System.IO.File.Exists(filepath))
            {
                return NotFound("File not found");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contentType, Path.GetFileName(filepath));
        }

        // API để xóa file vật lý khỏi hệ thống
        [HttpDelete]
        [Route("DeleteFile")]
        public IActionResult DeleteFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return BadRequest("Filename not provided.");
            }

            var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", filename);

            if (System.IO.File.Exists(filepath))
            {
                try
                {
                    // Xóa file từ hệ thống
                    System.IO.File.Delete(filepath);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting file: {ex.Message}");
                }
            }
            else
            {
                return NotFound("File not found.");
            }

            return NoContent();
        }

        // Hàm phụ trợ để ghi file vào hệ thống
        private async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            try
            {
                var extension = Path.GetExtension(file.FileName);
                filename = $"{DateTime.Now.Ticks}{extension}";

                var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(filepath, filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                return ex.Message;
            }
            return filename;
        }
    }
}
