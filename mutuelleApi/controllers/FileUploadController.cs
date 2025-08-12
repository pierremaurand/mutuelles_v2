using Microsoft.AspNetCore.Mvc;
using System.IO;
using mutuelleApi.dtos;

namespace mutuelleApi.controllers
{
    public class FileUploadController(IWebHostEnvironment webHost) : BaseController
    {
        private readonly IWebHostEnvironment _env = webHost;

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadImage fichier)
        {

            try
            {
                // Define the upload directory (e.g., within WebRootPath)
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique file name to prevent overwrites and security issues
                string fileName = Guid.NewGuid().ToString() + '.' + fichier.Extension;
                string filePath = Path.Combine(uploadsFolder, fileName);
                
                byte[] bytes = Convert.FromBase64String(fichier.Image.Split(',')[1]);
                MemoryStream image = new MemoryStream(bytes);
        
                IFormFile file = new FormFile(image, 0, bytes.Length, "image", "image");

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { FileName = fileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}