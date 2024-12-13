using backend.Constants;
using backend.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Project.Entities;
using System.Configuration;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly HcmUeQTTB_DevContext _context;
        private readonly IConfiguration _configuration;

        public FilesController(IWebHostEnvironment hostingEnvironment, HcmUeQTTB_DevContext context, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _configuration = configuration;
        }

        // API để upload file và lưu URL vào bảng CriteriaReport
        //[HttpPost]
        //[Route("UploadFile")]
        //[ClaimPermission(PermissionConstants.ModifyReport)]
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("File not selected");
        //    }
        //    var result = await WriteFile(file);
        //    if (string.IsNullOrEmpty(result))
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "File upload failed.");
        //    }

        //    // Tạo URL cho file
        //    var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{result}";


        //    return Ok(new { FileUrl = fileUrl });

        //}

        //[HttpPost]
        //[Route("UploadFile")]
        //[ClaimPermission(PermissionConstants.ModifyReport)]
        //public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        //{
        //    if (files == null || !files.Any())
        //    {
        //        return BadRequest("No files selected");
        //    }

        //    var fileUrls = new List<string>();

        //    foreach (var file in files)
        //    {
        //        var result = await WriteFile(file);
        //        if (string.IsNullOrEmpty(result))
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, "File upload failed.");
        //        }

        //        // Tạo URL cho file
        //        var fileUrl = $"{Request.Scheme}://${Request.Host}/uploads/{result}";
        //        fileUrls.Add(fileUrl);
        //    }

        //    return Ok(new { FileUrls = fileUrls });
        //}

        [HttpGet]
        [Route("GetImage")]
        public IActionResult GetImage([FromQuery] string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                return BadRequest("File URL is required.");
            }

            try
            {
                // Trích xuất tên file từ URL
                var fileName = Path.GetFileName(fileUrl);

                // Đường dẫn tới thư mục chứa ảnh
                var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploadPath, fileName);

                // Kiểm tra file có tồn tại không
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Image not found.");
                }

                // Xác định loại nội dung của ảnh
                var fileExtension = Path.GetExtension(fileName).ToLower();
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                {
                    return BadRequest("The specified file is not a valid image.");
                }

                var mimeType = fileExtension switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    _ => "application/octet-stream"
                };

                // Trả về ảnh
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, mimeType, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving image: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the image.");
            }
        }


        [HttpPost]
        [Route("UploadFiles")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest("No files selected");
            }

            var fileUrls = new List<string>();
            var Name = new List<string>();
            var baseUrl = _configuration["BaseUrl"]; // Lấy giá trị BaseUrl từ appsettings.json

            foreach (var file in files)
            {
                var result = await WriteFile1(file);
                if (string.IsNullOrEmpty(result))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "File upload failed.");
                }

                // Chỉ lấy tên file từ kết quả
                var fileName = Path.GetFileName(result);

                // Tạo URL truy cập file
                var fileUrl = $"{baseUrl}/uploads/{fileName}";
                fileUrls.Add(fileUrl);

                Name.Add(fileName);
            }

            return Ok(new { FileUrls = Name });
        }


        private async Task<string?> WriteFile1(IFormFile file)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".mp4", ".csv" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return null;
                }

                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
                return null;
            }
        }

        // API để tải file từ server và trả về file
        [HttpGet]
        [Route("DownloadFile")]
       // [ClaimPermission(PermissionConstants.ViewReport)]
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
        [ClaimPermission(PermissionConstants.ModifyReport)]
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
